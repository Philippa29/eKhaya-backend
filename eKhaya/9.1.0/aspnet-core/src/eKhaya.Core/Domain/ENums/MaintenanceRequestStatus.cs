using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Domain.ENums
{
    public enum MaintenanceRequestStatus : int
    {
        [Description("Pending")]
        Available = 1,

        [Description("Assigned")]
        Unavailable = 2,

        [Description("Completed")]
        Requested = 3,
    }
}
