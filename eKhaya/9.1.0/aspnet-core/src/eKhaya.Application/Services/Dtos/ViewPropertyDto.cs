using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Services.Dtos
{
    public class ViewPropertyDto
    {
        public Guid PropertyId { get; set; }
        public string PropertyName { get; set; }

        public string Description { get; set; }

        public List<string> Amenities { get; set; }

        public string Base64Image { get; set; }


    }
}
