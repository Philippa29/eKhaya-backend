using Abp.Application.Services;
using eKhaya.Domain.Images;
using eKhaya.Services.Dtos;

using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Services.ImagesService
{
    public interface IImagesAppService : IApplicationService
    {

      Task<Image> CreateImage(ImagesDto input);

      Task<IActionResult> UpdateImage(Guid id , Image image);

        
        

        Task<List<FileDto>> GetImagesForOwner(Guid id);
        

    }
}
