using Abp.Domain.Entities.Auditing;
using eKhaya.Domain.Properties;
using eKhaya.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Domain.AgentsProperty
{
    public class AgentProperty : FullAuditedEntity<Guid>
    {
            public virtual Agent Agent { get; set; }
            public virtual Property Property { get; set; }
        

    }
}
