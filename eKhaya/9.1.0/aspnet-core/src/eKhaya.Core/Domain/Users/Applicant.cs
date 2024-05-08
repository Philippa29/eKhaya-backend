using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Domain.Users
{
    public class Applicant : Person
    {
        [StringLength(13)]
        public virtual string ApplicantID { get; set; }
    }
}
