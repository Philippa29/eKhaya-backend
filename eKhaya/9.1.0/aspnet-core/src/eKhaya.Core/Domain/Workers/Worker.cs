using Abp.Domain.Entities.Auditing;
using eKhaya.Domain.ENums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Domain.Workers
{
    public class Worker : FullAuditedEntity<Guid>
    {
        public virtual string Name { get; set; }

        public virtual string Surname { get; set; }

        public virtual string PhoneNumber { get; set; }

        public virtual WorkerType WorkerType { get; set; }


    }
}
