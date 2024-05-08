using Abp.Domain.Entities;
using eKhaya.Domain.ENums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Services.Dtos
{
    public class WorkerDto : Entity<Guid>
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string PhoneNumber { get; set; }

        public WorkerType WorkerType { get; set; }
    }
}
