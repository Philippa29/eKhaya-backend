using Abp.Domain.Entities;
using eKhaya.Domain.ENums;
using eKhaya.Domain.Leases;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Domain.LeaseDocument
{
    public class LeaseDocument : Entity<Guid>
    {

        public virtual Lease OwnerID { get; set; }

        [NotMapped]
        public virtual IFormFile File { get; set; }


        public virtual string DocumentName { get; set; }


        public virtual string FileType { get; set; }

       


    }
}
