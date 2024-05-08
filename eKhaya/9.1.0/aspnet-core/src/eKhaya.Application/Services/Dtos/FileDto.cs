using eKhaya.Domain.ENums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Services.Dtos
{
    public class FileDto
    {

        public Guid Id { get; set; }

        public Guid OwnerId { get; set; }
        public string FileName { get; set; }
        public ImageType ImageType { get; set; }
        public string Base64 { get; set; }
    }
}
