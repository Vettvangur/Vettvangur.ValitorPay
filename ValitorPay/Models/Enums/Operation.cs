using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vettvangur.ValitorPay.Models.Enums
{
    public enum Operation
    {
        Sale,
        Refund,
        Auth,
        Capture,
        CaptureRefund,
    }
}
