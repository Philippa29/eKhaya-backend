using Abp.Domain.Entities.Auditing;
using eKhaya.Domain.Units;
using eKhaya.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Domain.UnitsAgents
{
    public class UnitsAgents : FullAuditedEntity<Guid>
    {
        public Unit Unit { get; set; }
        public Agent Agent { get; set; }
    }
}
