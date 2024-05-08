using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Domain.ENums
{
    public enum PaymentType : int
    {
        [Description("Rent")]
        Rent = 1,

        [Description("Deposit")]
        Deposit = 2,

        [Description("Other")]
        Other = 3,
       
    }
}
