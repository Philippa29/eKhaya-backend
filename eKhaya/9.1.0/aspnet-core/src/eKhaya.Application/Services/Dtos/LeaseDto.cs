using Abp.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Services.Dtos
{
    public class LeaseDto : Entity<Guid>
    {
       

        public decimal RentAmount { get; set; }

        public bool DepositPaid { get; set; }

        public Guid OwnerID { get; set; }

        [NotMapped]

        public IFormFile File { get; set; }
    }
}
