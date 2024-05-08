using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Domain.ENums
{
    public enum MaintenanceType : int
    {
        [Description("Plumbing")]
        Plumbing = 1,

        [Description("Electrical")]
        Electrical = 2,

        [Description("Structural")]
        Structural = 3,
    }
}
