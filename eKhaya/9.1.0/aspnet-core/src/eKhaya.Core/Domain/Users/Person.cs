using Abp.Domain.Entities.Auditing;
using eKhaya.Authorization.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Domain.Users
{
    public class Person : FullAuditedEntity<Guid>
    {
        public virtual  string Name { get; set; }
        public virtual string Surname { get; set; }

        public virtual string EmailAddress { get; set; }

        public virtual string PhoneNumber { get; set; }

        public virtual User User { get; set; }


    }
}
