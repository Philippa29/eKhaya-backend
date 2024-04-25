using Abp.Domain.Entities.Auditing;
using eKhaya.Domain.Amenities;
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
        
        public Unit Unit { get; set; }
        public Amenity Amenity { get; set; }
    }
}
