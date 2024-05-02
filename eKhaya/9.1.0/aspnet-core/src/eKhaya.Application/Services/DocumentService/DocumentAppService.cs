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
        public async Task<Document> CreateDocAsync( [FromForm] DocumentDto input)
        {
            var document = _mapper.Map<Document>(input);
            document.FileType = input.File.ContentType;

            // Check if the file type is PDF
            if (IsPdf(input.File.ContentType))
            {
                var filePath = $"{BASE_FILE_PATH}/{document.File.FileName}";

                using (var fileStream = input.File.OpenReadStream())
                {
                    await SaveFile(filePath, fileStream);
                }

                document.DocumentName = document.File.FileName;
                document.FileType = document.File.ContentType;

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

        public async Task<List<AllDocumentsDto>> GetAllDocumentsAsync()
        {
            var documents = await _documentRepository
                .GetAll()
                .ToListAsync();

            return _mapper.Map<List<AllDocumentsDto>>(documents);
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


        [Route("api/documents")]
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






