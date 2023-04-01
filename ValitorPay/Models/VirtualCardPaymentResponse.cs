using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Vettvangur.ValitorPay
{
    public class VirtualCardPaymentResponse : ValitorResponseBase
    {
        [JsonPropertyName("authorizationResponseTime")]
        public string AuthorizationResponseTime { get; set; }

        [JsonPropertyName("acquirerReferenceNumber")]
        public string AcquirerReferenceNumber { get; set; }

        [JsonPropertyName("transactionID")]
        public string TransactionId { get; set; }

        [JsonPropertyName("authorizationCode")]
        public string AuthorizationCode { get; set; }

        [JsonPropertyName("transactionLifecycleId")]
        public string TransactionLifecycleId { get; set; }

        [JsonPropertyName("maskedCardNumber")]
        public string MaskedCardNumber { get; set; }

        [JsonPropertyName("correlationID")]
        public Guid CorrelationId { get; set; }
    }
}
