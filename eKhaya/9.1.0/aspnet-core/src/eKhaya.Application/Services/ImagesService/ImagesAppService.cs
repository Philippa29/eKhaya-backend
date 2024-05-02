using Abp.Application.Services;

using Abp.Domain.Repositories;
using Abp.UI;
using AutoMapper;
using eKhaya.Domain.Images;
using eKhaya.Domain.Properties;
using eKhaya.Domain.Units;
using eKhaya.Helper;
using eKhaya.Services.Dtos;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;



namespace eKhaya.Services.ImagesService
{
    public class ImagesAppService : ApplicationService, IImagesAppService
    {


        const string BASE_IMAGE_PATH = "App_Data/Images";

        private readonly IRepository<Image, Guid> _imagesRepository;
        private readonly IRepository<Property, Guid> _propertyRepository;
        private readonly IRepository<Unit, Guid> _unitRepository;
        private readonly IMapper _mapper;

        public ImagesAppService(IRepository<Image, Guid> imagesRepository, IRepository<Property, Guid> propertyRepository, IRepository<Unit, Guid> unitRepository, IMapper mapper)
        {
            _imagesRepository = imagesRepository;
            _propertyRepository = propertyRepository;
            _unitRepository = unitRepository;
            _mapper = mapper;
        }
        [HttpPost ]
        [Consumes("multipart/form-data")]
        public async Task<Image> CreateImage([FromForm]ImagesDto input)
        {
            var image = new Image
            {
                OwnerID = input.OwnerID,
                ImageName = input.File.FileName,
                ImageType = input.File.ContentType,

            }; 
            

            var imagePath = $"{BASE_IMAGE_PATH}/{image.ImageName}";

            using (var stream = input.File.OpenReadStream())
            {
                await SaveFile(imagePath, stream);
            }

            image.ImageName = input.File.FileName;
            image.ImageType = input.File.ContentType;

            return await _imagesRepository.InsertAsync(image);
               
        }

      

        public async Task<List<FileDto>> GetImagesForOwner(Guid id)
        {
            var images = await _imagesRepository.GetAllListAsync(x => x.OwnerID == id);

            var response = new List<FileDto>();

            if (images == null)
                throw new UserFriendlyException("File not found");

            foreach (var image in images)
            {
                var imagePath = $"{BASE_IMAGE_PATH}/{image.ImageName}";

                byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);
                string base64String = Convert.ToBase64String(imageBytes);

                response.Add(new FileDto
                {
                    Id = image.Id,
                    FileName = image.ImageName,
                    FileType = image.ImageType,
                    OwnerId = image.OwnerID,
                    Base64 = base64String
                    
                });
            }

            return response;
        }

        public async Task DeleteImage (Guid id)
        {
            var image = await _imagesRepository.GetAsync(id);
            var imagePath = $"{BASE_IMAGE_PATH}/{image.ImageName}";

            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath); 
            }
            if (image == null)
            {
                throw new Exception("Image not found");
            }

            await _imagesRepository.DeleteAsync(image);
        }
        public async Task<IActionResult> UpdateImage(Guid id, [FromForm] Image input)
        {
            // Retrieve the existing stored file from the database based on the provided GUID
            var existingStoredFile = await _imagesRepository.FirstOrDefaultAsync(x => x.Id == id);

            // Check if the file exists
            if (existingStoredFile == null)
            {
                
                throw new Exception("Image not found");
            }

            // Delete the old image file
            await DeleteImage(existingStoredFile.Id);

            // Process the new image and save it to the appropriate location
            var newFilePath = await SaveFileForUpdate(input.File);

            // Update the properties of the existing stored file with the new information
            existingStoredFile.ImageName = input.File.FileName;
            existingStoredFile.ImageType = input.File.ContentType;
            existingStoredFile.OwnerID = input.OwnerID;
            // Update any other relevant properties as needed

            // Save the changes to the database
            await _imagesRepository.UpdateAsync(existingStoredFile);

            return new OkResult();
        }

        private async Task<string> SaveFileForUpdate(IFormFile newImage)
        {
            if (!Utils.IsImage(newImage))
            {
                throw new ArgumentException("The file is not a valid image.");
            }

            //var timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            var imageName = newImage.FileName;
            var imagePath = Path.Combine(BASE_IMAGE_PATH, imageName);

            using (var fileStream = newImage.OpenReadStream())
            using (var fs = new FileStream(imagePath, FileMode.Create))
            {
                await fileStream.CopyToAsync(fs);
            }

            return imagePath;
        }

        private async Task SaveFile(string filePath, Stream stream)
        {
            using (var fs = new FileStream(filePath, FileMode.Create))
            {
                await stream.CopyToAsync(fs);
            }
        }



    }
}
