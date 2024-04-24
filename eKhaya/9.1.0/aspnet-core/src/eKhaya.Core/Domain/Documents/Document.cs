using Abp.Domain.Entities;
using eKhaya.Domain.ENums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Domain.Documents
{
    public class Document : Entity<Guid>
    {

        [NotMapped]
        public virtual IFormFile File { get; set; }


        public virtual string DocumentName { get; set; }


        public virtual string FileType { get; set; }

        public virtual DocumentType DocumentType { get; set; }


    }
}
