using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Vettvangur.ValitorPay
{
    public class CardVerificationResponse : ValitorResponseBase
    {
        [JsonPropertyName("cardVerificationRawResponse")]
        public string CardVerificationRawResponse { get; set; }

        [JsonPropertyName("cardInformation")]
        public object CardInformation { get; set; }

        [JsonPropertyName("correlationID")]
        public Guid CorrelationId { get; set; }
    }
}
