using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using eKhaya.Domain.Documents;
using eKhaya.Domain.ENums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Services.Dtos
{
   

    public class DocumentDto 

    {
        [AutoMap(typeof(Document))]

        public IFormFile File { get; set; }


        public Guid OwnerID { get; set; }

        public DocumentType DocumentType { get; set; }

    }
}
