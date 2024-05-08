using Abp.Application.Services.Dto;
using eKhaya.Domain.Amenities;
using eKhaya.Domain.ENums;
using eKhaya.Domain.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Services.Dtos
{
    public class UnitsAmenitiesDto : EntityDto<Guid>
    {
        public Guid Property { get; set; }

        public UnitType UnitType { get; set; }
        public List<Guid> Amenity { get; set; }
    }
}
