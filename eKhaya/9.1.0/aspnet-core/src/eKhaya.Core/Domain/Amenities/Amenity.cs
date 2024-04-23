using Abp.Domain.Entities.Auditing;
using eKhaya.Domain.ENums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Domain.Amenities
{
    public class Amenity : FullAuditedEntity<Guid>
    {
        public virtual string Name { get; set; }

        public virtual AmenitiesType Description { get; set; }


    }
}
