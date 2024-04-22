using Abp.Domain.Entities.Auditing;
using eKhaya.Domain.Amenities;
using eKhaya.Domain.Images;
using eKhaya.Domain.Units;
using eKhaya.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Domain.Properties
{
    public class Property : FullAuditedEntity<Guid>
    {
        public virtual string Address { get; set; }

        public virtual decimal Size { get; set; }
        
        public virtual ICollection<Image> Images { get; set; }

        public virtual PropertyManager PropertyManager { get; set; }

        public virtual ICollection<Agent> Agents { get; set; }

        public virtual ICollection<PropertyAmenity> Amenitity { get; set; }

    }
}
