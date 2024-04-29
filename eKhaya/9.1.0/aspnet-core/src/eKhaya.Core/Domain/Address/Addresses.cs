using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Domain.Address
{
    public class Addresses : FullAuditedEntity<Guid>
    {
        [StringLength(200)]
        
        public virtual string AddressLine1 { get; set; }

        [StringLength(200)]
        public virtual string AddressLine2 { get; set; }

        [StringLength(200)]
        public virtual string AddressLine3 { get; set; }

        [StringLength(100)]
        public virtual string Suburb { get; set; }

        [StringLength(100)]
        public virtual string Town { get; set; }

        [StringLength(10)]
        public virtual string POBox { get; set; }

        [Range(-90, 90, ErrorMessage = "Latitude should be in range (-90, 90)")]
        public virtual decimal? Latitude { get; set; }

        [Range(-90, 90, ErrorMessage = "Longitude should be in range (-90, 90)")]
        public virtual decimal? Longitude { get; set; }

    }
}
