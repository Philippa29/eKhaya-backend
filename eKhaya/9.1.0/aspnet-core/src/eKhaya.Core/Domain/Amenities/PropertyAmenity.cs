using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Domain.Amenities
{
    public class PropertyAmenity : FullAuditedEntity<Guid>
    {
        public virtual string Name { get; set; }


    }
}
