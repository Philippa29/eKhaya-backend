using Abp.Domain.Entities.Auditing;
using eKhaya.Domain.Amenities;
using eKhaya.Domain.ENums;
using eKhaya.Domain.Images;
using eKhaya.Domain.Properties;
using eKhaya.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Domain.Units
{
    public class Unit : FullAuditedEntity<Guid>
    {
        public virtual int Size { get; set; }

        public virtual UnitType Type { get; set; }

        public virtual Agent AgentID { get; set; }

        public virtual string UnitNumber { get; set; }

        public virtual ICollection<Amenity> Amenitities { get; set; }

        public virtual int Level { get; set; } 

        public virtual bool Availability { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        public virtual Property PropertyID { get; set; }
    }
}
