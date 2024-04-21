using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Domain.Users
{
    public class Agent : Person
    {
        public virtual string AgentID { get; set; }
    }
}
