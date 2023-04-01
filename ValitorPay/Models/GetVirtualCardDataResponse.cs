using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Vettvangur.ValitorPay.Models
{
    public class GetVirtualCardDataResponse : ValitorResponseBase
    {
        [JsonPropertyName("expirationMonth")]
        public string ExpirationMonth { get; set; }
        [JsonPropertyName("expirationYear")]
        public string ExpirationYear { get; set; }
        [JsonPropertyName("transactionLifecycleId")]
        public string TransactionLifecycleId { get; set; }
        [JsonPropertyName("btoredCredential")]
        public bool StoredCredential { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("subsequentTransactionType")]
        public SubsequentTransactionType SubsequentTransactionType { get; set; }
        [JsonPropertyName("sponsoredMerchantId")]
        public string SponsoredMerchantId { get; set; }
        [JsonPropertyName("maskedCardNumber")]
        public string MaskedCardNumber { get; set; }
        [JsonPropertyName("virtualCard")]
        public string VirtualCard { get; set; }
    }
}
