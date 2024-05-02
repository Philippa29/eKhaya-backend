using Abp.Domain.Entities;
using eKhaya.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Services.Dtos
{
    public class UpdatePropertyDto : Entity<Guid>
    {
        public decimal Size { get; set; }
        public Guid PropertyManagerId { get; set; }


        public string PropertyName { get; set; }

        public string Description { get; set; }

    }
}