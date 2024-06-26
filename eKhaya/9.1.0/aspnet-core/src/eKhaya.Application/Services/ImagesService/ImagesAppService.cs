﻿using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.UI;
using AutoMapper;
using eKhaya.Domain.ENums;
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
        public async Task<Image?> CreateImage([FromForm] ImagesDto input)
        {
            var image = new Image
            {
                OwnerID = input.OwnerID,
                ImageName = input.File.FileName,
                ImageType = input.ImageType
            };

            if (input.OwnerID != null)
            {
                string imagePath;
                int totalCount;

                switch (input.ImageType)
                {
                    case ImageType.Property:
                        // Check if property exists
                        var property = await _propertyRepository.GetAsync(input.OwnerID);
                        if (property != null)
                        {
                            // Get the count of images for the property
                            totalCount = await _imagesRepository.CountAsync(img => img.OwnerID == input.OwnerID && img.ImageType == ImageType.Property);
                            if (totalCount >= 3)
                            {
                                throw new Exception("Cannot add more than 3 images for a property");
                            }
                            // Construct image path
                            imagePath = $"property_{totalCount + 1}_{image.ImageName}";
                        }
                        else
                        {
                            throw new Exception("Property not found");
                        }
                        break;
                    case ImageType.Unit_Bachelor:
                    case ImageType.Unit_1Bedroom:
                    case ImageType.Unit_2Bedroom:
                        // Check if unit exists

                        var property_unit = await _propertyRepository.GetAsync(input.OwnerID);
                        if (property_unit != null)
                        {
                            // Get the count of images for the unit
                            totalCount = await _imagesRepository.CountAsync(img => img.OwnerID == input.OwnerID && img.ImageType == input.ImageType);
                            if (totalCount >= 4)
                            {
                                throw new Exception($"Cannot add more than 3 images for a {input.ImageType.ToString().Split('_')[1]} unit");
                            }
                            // Construct image path
                            imagePath = $"unit_{input.ImageType.ToString().ToLower()}_{totalCount + 1}_{image.ImageName}";
                        }
                        else
                        {
                            throw new Exception("Unit not found");
                        }
                        break;
                    default:
                        throw new Exception("Invalid image type");
                }

                // Construct the full image path
                var fullImagePath = $"{BASE_IMAGE_PATH}/{imagePath}";

                using (var stream = input.File.OpenReadStream())
                {
                    await SaveFile(fullImagePath, stream);
                }

                // Update the ImageName property with the relative image path
                image.ImageName = imagePath;

                // Save the image record in the database
                return await _imagesRepository.InsertAsync(image);
            }
            else
            {
                throw new Exception("OwnerID cannot be null");
            }
        }






        public async Task<List<FileDto>> GetImagesForOwner(Guid id)
        {
            var images = await _imagesRepository.GetAllListAsync(x => x.OwnerID == id);

            var response = new List<FileDto>();

            if (images == null)
                throw new UserFriendlyException("File not found");

            foreach (var image in images)
            {
                string imagePath;

                // Determine the owner type based on the OwnerID
                var property = await _propertyRepository.FirstOrDefaultAsync(x => x.Id == id);
                if (property != null)
                {
                    imagePath = $"{BASE_IMAGE_PATH}/{image.ImageName}";
                }
                else
                {
                    var unit = await _unitRepository.FirstOrDefaultAsync(x => x.Id == id);
                    if (unit != null)
                    {
                        imagePath = $"{BASE_IMAGE_PATH}/{image.ImageName}";
                    }
                    else
                    {
                        throw new UserFriendlyException("Owner not found");
                    }
                }

                byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);
                string base64String = Convert.ToBase64String(imageBytes);

                response.Add(new FileDto
                {
                    Id = image.Id,
                    FileName = image.ImageName,
                    ImageType = image.ImageType,
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
            existingStoredFile.ImageType = input.ImageType;
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
