using Abp.Domain.Entities.Auditing;
using eKhaya.Domain.Amenities;
using eKhaya.Domain.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Domain.PropertyAmenities
{
    public class PropertyAmenity :FullAuditedEntity<Guid>
    {
            // Foreign key to Property
            public virtual Property Property { get; set; }

            // Foreign key to Amenity
            public virtual Amenity Amenity { get; set; }
        

    }
}
