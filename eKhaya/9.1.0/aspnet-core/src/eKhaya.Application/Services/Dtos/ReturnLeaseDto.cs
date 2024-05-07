using eKhaya.Domain.ENums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Services.Dtos
{
    public class ReturnLeaseDto
    {


        public Guid Id { get; set; }

        public Guid OwnerId { get; set; }
        public string FileName { get; set; }

        public string Base64 { get; set; }

        public decimal RentAmount { get; set; }

        public bool DepositPaid { get; set; }

        public Guid OwnerID { get; set; }

        public Guid TenantId { get; set; }

        public string LeaseName { get; set; }

        public string ResidentName {get ; set; }

        public string ResidentSurname { get; set; }

        public string EmailAddress { get; set; }



    }
}
