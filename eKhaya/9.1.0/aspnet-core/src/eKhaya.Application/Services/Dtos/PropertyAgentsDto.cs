using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using eKhaya.Domain.Properties;
using eKhaya.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Services.Dtos
{
    public class PropertyAgentsDto : EntityDto<Guid>
    {
        public Guid Agent { get; set; }
        public Guid Property { get; set; }
    }
}
