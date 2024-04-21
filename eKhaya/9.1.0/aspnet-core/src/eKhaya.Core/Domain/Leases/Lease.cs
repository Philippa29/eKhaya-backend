using Abp.Domain.Entities.Auditing;
using System;
using eKhaya.Domain.Users;
using eKhaya.Domain.Units;

namespace eKhaya.Domain.Leases
{
    public class Lease : FullAuditedEntity<Guid>
    {
        public virtual Guid LeaseID { get; set; }

        public virtual DateTime StartDate { get; set; }

        public virtual DateTime EndDate { get; set;}

        public virtual int RentAmount { get; set; }

        public virtual bool DepositPaid { get; set; }

        public virtual Unit Unit { get; set; }

        public virtual Resident Tenant { get; set; }

        
    }
}
