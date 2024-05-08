using eKhaya.Domain.ENums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Services.Dtos
{
    public class ViewUnitsPerPropertyDto
    {

        public Guid PropertyId { get; set; }

        public UnitType UnitType { get; set; }

        public List<string> Amenities { get; set; }

        public List<string> Base64Images { get; set; }
    }
}
