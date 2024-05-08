using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Domain.Users
{
    public class Resident : Person
    {
        public virtual string TenantID { get; set; }
    }
}
