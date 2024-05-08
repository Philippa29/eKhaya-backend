using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Domain.ENums
{
    public enum ApplicationStatus : int
    {
        Pending = 1,
        UnderReview = 2,
        Approved = 3,
        Rejected = 4,
    }
}
