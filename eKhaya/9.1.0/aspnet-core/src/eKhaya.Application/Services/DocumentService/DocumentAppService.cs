using Abp.Application.Services;
using Abp.Domain.Repositories;
using AutoMapper;

using eKhaya.Domain.Applications;

using eKhaya.Domain.Documents;
using eKhaya.Domain.ENums;
using eKhaya.Services.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Services.DocumentAppService
{
    public class DocumentAppService : ApplicationService, IDocumentAppService
    {

        const string BASE_FILE_PATH = "App_Data/Documents";

        private readonly IRepository<Document, Guid> _documentRepository;
        private readonly IRepository<Application, Guid> _applicationRepository;
        private readonly IMapper _mapper;

        
        public DocumentAppService(IRepository<Document, Guid> documentRepository, IRepository<Application, Guid> applicationRepository, IMapper mapper)
        {
            _documentRepository = documentRepository;
            _applicationRepository = applicationRepository;
            _mapper = mapper;
        }

        [Route("api/documents")]

        public async Task<Document> CreateDocAsync([FromForm] DocumentDto input)
        {
            var document = _mapper.Map<Document>(input);
            document.FileType = input.File.ContentType;

            var application = await _applicationRepository.GetAsync(input.OwnerID);

            if (application == null)
            {
                throw new Exception("Application not found");
            }

            // Check if the file type is PDF
            if (IsPdf(input.File.ContentType))
            {
                var documentType = Enum.GetName(typeof(DocumentType), input.DocumentType);

                var totalcount = await _documentRepository.CountAsync();
                var filePath = Path.Combine($"{documentType}_{totalcount + 1}_{input.File.FileName}");
                var fullImagePath = $"{BASE_FILE_PATH}/{filePath}";
                using (var fileStream = input.File.OpenReadStream())
                {
                    await SaveFile(fullImagePath, fileStream);
                }

                document.OwnerID = application;
                document.DocumentName = filePath; 
                document.DocumentType = input.DocumentType;


                return await _documentRepository.InsertAsync(document);
            }
            else
            {
                // Handle the case where the file type is not PDF
                throw new Exception("Only PDF files are allowed.");
            }
        }

        private bool IsPdf(string contentType)
        {
            // List of MIME types for PDF files
            var pdfMimeTypes = new List<string>
            {
                "application/pdf",
                "application/x-pdf"
                // Add more PDF MIME types if needed
            };

            // Check if the content type matches any of the PDF MIME types
            return pdfMimeTypes.Contains(contentType);
        }

        [Route("api/alldocuments")]

        public async Task<List<AllDocumentsDto>> GetAllDocumentsAsync(Guid id)
        {
            var contentResults = new List<FileStreamResult>();
            var response = new List<AllDocumentsDto>();

            var files = _documentRepository.GetAllIncluding(x => x.OwnerID).Where(x => x.OwnerID.Id == id).ToList();
            if(files == null)
            {
                throw new Exception("No files found");
            }

            foreach (var file in files)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), BASE_FILE_PATH, file.DocumentName);

                byte[] fileBytes = System.IO.File.ReadAllBytes(path);
                string base64String = Convert.ToBase64String(fileBytes);

                response.Add(new AllDocumentsDto
                {
                    Id = file.Id,
                    FileName = file.DocumentName,
                    FileType = file.FileType,
                    Base64 = base64String
                });
                
            }

            return response;
        }

        public async Task<string> GetDocumentsAsync(Guid id)
        {
            var document = await _documentRepository.GetAsync(id);
            if (document == null)
            {
                return null; // or throw an exception
            }

            var filePath = $"{BASE_FILE_PATH}/{document.DocumentName}";
            return File.ReadAllText(filePath);
        }



        [Route("api/getdocuments")]

        public async Task<List<DocumentDto>> GetDocumentsAsync()
        {
            var documents = await _documentRepository
                .GetAll()
                .ToListAsync();

            return _mapper.Map<List<DocumentDto>>(documents);
        }

        public async Task<IActionResult> UpdateDocumentAsync(Guid id , [FromForm]DocumentDto input)
        {
            var existingDocument = await _documentRepository.GetAsync(id);
            if (existingDocument == null)
            {
                return new NotFoundResult();
            }

            _mapper.Map(input, existingDocument);
            await _documentRepository.UpdateAsync(existingDocument);
            return new OkResult();
        }

        public async Task DeleteDocumentAsync(Guid id)
        {
            var document = await _documentRepository.GetAsync(id);
            if (document != null)
            {
                var filePath = $"{BASE_FILE_PATH}/{document.DocumentName}";
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                await _documentRepository.DeleteAsync(document);
            }
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






