using Abp.Application.Services.Dto;
using eKhaya.Domain.ENums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Services.Dtos
{
    public class AmenitiesDto : EntityDto<Guid>
    {
        public string Name { get; set; }

        public AmenitiesType Type { get; set; }
    }
}
