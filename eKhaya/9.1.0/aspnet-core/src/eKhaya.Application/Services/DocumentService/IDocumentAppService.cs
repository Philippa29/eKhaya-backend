using Abp.Application.Services;
using eKhaya.Domain.Documents;
using eKhaya.Services.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Services.DocumentAppService
{
    public interface IDocumentAppService : IApplicationService
    {

        Task<Document> CreateDocAsync( DocumentDto input); 

        Task<List<AllDocumentsDto>> GetAllDocumentsAsync(Guid id);


        Task<string> GetDocumentsAsync(Guid id);

        Task<List<DocumentDto>> GetDocumentsAsync();
        Task<IActionResult> UpdateDocumentAsync(Guid id , DocumentDto input);
        Task DeleteDocumentAsync(Guid id);

    }

}
