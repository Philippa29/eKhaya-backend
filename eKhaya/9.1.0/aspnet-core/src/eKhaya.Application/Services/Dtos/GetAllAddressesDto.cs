using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Services.Dtos
{
    public class GetAllAddressesDto : EntityDto<Guid>
    {

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string Suburb { get; set; }
        public string Town { get; set; }
        public string POBox { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public Guid PropertyId { get; set; }

        public string PropertyName { get; set; }
    }
}
