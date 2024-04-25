using Abp.Application.Services.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Services.Dtos
{
    public class ImagesDto : EntityDto<Guid>
    {
        public Guid OwnerID { get; set; }
        
        public IFormFile File { get; set; }
    }
}
