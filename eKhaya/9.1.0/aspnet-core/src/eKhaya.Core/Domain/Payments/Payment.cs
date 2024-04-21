using Abp.Domain.Entities.Auditing;
using eKhaya.Domain.ENums;
using eKhaya.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Domain.Payment
{
    public class Payment : FullAuditedEntity<Guid>
    {
        public virtual PaymentType PaymentType { get; set; }

        public virtual DateTime PaymentDate { get; set; }

        public virtual Resident Tenant { get; set; }

        public virtual double Amount { get; set; }


    }
}
