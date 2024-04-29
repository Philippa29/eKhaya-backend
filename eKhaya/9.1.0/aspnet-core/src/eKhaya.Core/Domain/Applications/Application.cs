using Abp.Domain.Entities.Auditing;
using eKhaya.Domain.Documents;
using eKhaya.Domain.ENums;
using eKhaya.Domain.Units;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Domain.Applications
{
    public class Application : FullAuditedEntity<Guid>
    {
        public virtual Unit Unit { get; set; }

        public virtual ApplicationStatus ApplicationStatus { get; set; }

        public virtual Document ID { get; set; }

        public virtual Document Payslip { get; set; }

        public virtual Document BankStatement { get; set; }

        public virtual string Description { get; set; }


        
    }
}
