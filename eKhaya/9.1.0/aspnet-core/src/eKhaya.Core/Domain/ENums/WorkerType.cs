using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Domain.ENums
{
    public enum WorkerType : int
    {
        [Description("Plumber")]
        Available = 1,

        [Description("Electrician")]
        Unavailable = 2,

        [Description("Builder")]
        Requested = 3,
    }
}
