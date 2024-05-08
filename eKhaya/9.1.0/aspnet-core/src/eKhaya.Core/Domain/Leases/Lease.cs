using Abp.Domain.Entities.Auditing;
using System;
using eKhaya.Domain.Users;
using eKhaya.Domain.Units;
using eKhaya.Domain.ENums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace eKhaya.Domain.Leases
{
    public class Lease : FullAuditedEntity<Guid>
    {

        public virtual Unit Unit { get; set; }

        public virtual Guid Tenant { get; set; }

        public virtual Agent Agent { get; set;  }



    }
}
