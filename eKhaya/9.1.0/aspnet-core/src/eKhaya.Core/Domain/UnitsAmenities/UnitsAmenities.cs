using Abp.Domain.Entities.Auditing;
using eKhaya.Domain.Amenities;
using eKhaya.Domain.ENums;
using eKhaya.Domain.Properties;
using eKhaya.Domain.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Domain.UnitsAmenities
{
    public class UnitsAmenities : FullAuditedEntity<Guid>
    {

        public Property Property { get; set; }

        public UnitType UnitType { get; set; } 
        public Amenity Amenity { get; set; }
    }
}
