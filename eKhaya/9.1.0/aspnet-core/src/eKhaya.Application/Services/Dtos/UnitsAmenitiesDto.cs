using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Services.Dtos
{
    public class UnitsAmenitiesDto : EntityDto<Guid>
    {
        public Guid UnitId { get; set; }
        public Guid AmenityId { get; set; }
    }
}
