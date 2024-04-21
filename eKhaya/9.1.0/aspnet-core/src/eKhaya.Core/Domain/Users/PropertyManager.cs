using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Domain.Users
{
    public class PropertyManager : Person
    {
        public virtual string PropertyManagerID { get; set; }
    }
}
