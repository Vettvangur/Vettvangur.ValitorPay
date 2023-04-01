using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Vettvangur.ValitorPay
{
    public class ValitorRequestBase
    {
        [JsonPropertyName("systemCalling")]
        public string SystemCalling { get; set; } = Constants.SystemCalling;

        [JsonPropertyName("agreementNumber")]
        public string AgreementNumber { get; set; }

        [JsonPropertyName("terminalId")]
        public string TerminalId { get; set; }
    }
}
