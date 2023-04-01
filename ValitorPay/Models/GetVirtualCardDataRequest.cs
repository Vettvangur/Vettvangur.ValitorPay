using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vettvangur.ValitorPay.Models
{
    public class GetVirtualCardDataRequest : ValitorRequestBase
    {
        public string VirtualCard { get; set; }
    }
}
