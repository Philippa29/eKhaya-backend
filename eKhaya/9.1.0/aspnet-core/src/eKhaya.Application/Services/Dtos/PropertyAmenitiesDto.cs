using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Services.Dtos
{
    public class PropertyAmenitiesDto : EntityDto<Guid>
    {
        public Guid Property { get; set; }

        public Guid Amenities { get; set; }
    }
}
