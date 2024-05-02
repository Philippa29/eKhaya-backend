using Abp.Domain.Entities;
using eKhaya.Domain.ENums;
using eKhaya.Domain.Properties;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Domain.Images
{
    public class Image : Entity<Guid>
    {

            public virtual Guid OwnerID { get; set; }  
            [NotMapped]
            public virtual IFormFile File { get; set; }


            public virtual string ImageName { get; set; }


            public virtual ImageType ImageType { get; set; }
      
             
    }
}
