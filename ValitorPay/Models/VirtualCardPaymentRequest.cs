using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Vettvangur.ValitorPay.Models;
using Vettvangur.ValitorPay.Models.Enums;

namespace Vettvangur.ValitorPay
{
    public class VirtualCardPaymentRequest : ValitorRequestBase
    {
        [JsonPropertyName("operation")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Operation Operation { get; set; }

        /// <summary>
        /// Supports older numeric format and newer Guid format
        /// </summary>
        [JsonPropertyName("virtualCardNumber")]
        public string VirtualCardNumber { get; set; }

        [JsonPropertyName("initiationReason")]
        public object InitiationReason { get; set; }

        [JsonPropertyName("virtualCardPaymentAdditionalData")]
        public AdditionalData VirtualCardPaymentAdditionalData { get; set; }

        [JsonPropertyName("cardVerificationData")]
        public CardVerificationData CardVerificationData { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; } = "ISK";

        /// <summary>
        /// Remember to add "00" after ISK amounts
        /// </summary>
        [JsonPropertyName("amount")]
        public long Amount { get; set; }

        [JsonPropertyName("acquirerReferenceNumber")]
        public string AcquirerReferenceNumber { get; set; }

        [JsonPropertyName("authorizationCode")]
        public string AuthorizationCode { get; set; }

        [JsonPropertyName("sponsoredMerchantData")]
        public object SponsoredMerchantData { get; set; }

        [JsonPropertyName("authType")]
        public AuthType? AuthType { get; set; }

        [JsonPropertyName("delayedClearingData")]
        public string DelayedClearingData { get; set; }

        [JsonPropertyName("isFinalCapture")]
        public bool IsFinalCapture { get; set; }

        [JsonPropertyName("scaExemption")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ScaExemption ScaExemption { get; set; }
    }
}
