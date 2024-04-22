using Abp.Domain.Entities.Auditing;
using eKhaya.Domain.ENums;
using eKhaya.Domain.Units;
using eKhaya.Domain.Users;
using eKhaya.Domain.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Domain.MaintenanceRequests
{
    public class MaintenanceRequest : FullAuditedEntity<Guid>
    {

        public virtual MaintenanceType Type { get; set; }
        public virtual MaintenanceRequestStatus  Status { get; set; }

        public virtual DateTime CreatedDate { get; set; }

        public virtual DateTime DateCompleted { get; set; }

        public virtual Resident Tenant { get; set; }

        public virtual Unit UnitID { get; set; }

        public virtual Worker Worker { get; set; }





    }
}
