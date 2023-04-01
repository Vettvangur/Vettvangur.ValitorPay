using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Vettvangur.ValitorPay
{
    public class CreateVirtualCardResponse : ValitorResponseBase
    {
        [JsonPropertyName("virtualCard")]
        public Guid VirtualCard { get; set; }

        [JsonPropertyName("correlationID")]
        public Guid CorrelationId { get; set; }
    }
}
