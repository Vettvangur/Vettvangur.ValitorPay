using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Vettvangur.ValitorPay
{
    public class CardPaymentResponse : ValitorResponseBase
    {
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

        [JsonPropertyName("cardInformation")]
        public CardInformation CardInformation { get; set; }

        [JsonPropertyName("correlationID")]
        public Guid CorrelationId { get; set; }
    }

    public class CardInformation
    {
        [JsonPropertyName("cardScheme")]
        public string CardScheme { get; set; }

        [JsonPropertyName("issuingCountry")]
        public string IssuingCountry { get; set; }

        [JsonPropertyName("cardUsage")]
        public string CardUsage { get; set; }

        [JsonPropertyName("cardCategory")]
        public string CardCategory { get; set; }

        [JsonPropertyName("outOfScaScope")]
        public bool OutOfScaScope { get; set; }
    }
}
