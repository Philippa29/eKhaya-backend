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
        Plumber = 1,

        [Description("Electrician")]
        Electrician = 2,

        [Description("Builder")]
        Builder = 3,
    }
}
