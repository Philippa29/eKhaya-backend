using Abp.Domain.Entities.Auditing;
using eKhaya.Domain.Documents;
using eKhaya.Domain.ENums;
using eKhaya.Domain.Units;
using eKhaya.Domain.Users;
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

        public virtual Applicant Applicant { get; set; }

        public virtual ApplicationStatus ApplicationStatus { get; set; }

        public virtual MaritalStatus MaritalStatus { get; set; }

        public virtual CommunityType ComunityType { get; set; }
        public virtual string CompanyName { get; set; }

        public virtual string CompanyAddress { get; set; }

        public virtual string CompanyContactNumber { get; set; }

        public virtual string Occupation { get; set; }

        public virtual int Salary { get; set; }

        public virtual int MonthsWorked { get; set; }

        public virtual DateTime CreatedDate { get; set; }

        public virtual bool Insolvent { get; set; }

        public virtual bool Evicited { get; set; }
        public virtual string Declaration { get; set; }
        public virtual string TermsandConditions { get; set; }

        public virtual ApplicationType ApplicationType { get; set; }
    }
}
