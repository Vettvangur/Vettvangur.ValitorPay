using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Vettvangur.ValitorPay
{
    public enum SubsequentTransactionType
    {
        CardholderInitiatedCredentialOnFile,
        MerchantInitiatedCredentialOnFile,
        MerchantInitiatedRecurring,
        MerchantInitiatedInstallment,
    }

    public class CreateVirtualCardRequest : ValitorRequestBase
    {
        [Required]
        [JsonPropertyName("cardNumber")]
        public string CardNumber { get; set; }

        [JsonPropertyName("expirationMonth")]
        public int ExpirationMonth { get; set; }

        private int _expirationYear;
        [JsonPropertyName("expirationYear")]
        public int ExpirationYear 
        { 
            get => _expirationYear;
            set
            {
                if (value < 1000)
                {
                    _expirationYear = value + 2000;
                }
                else
                {
                    _expirationYear = value;
                }
            }
        }

        [Required]
        [JsonPropertyName("cvc")]
        public string Cvc { get; set; }

        [JsonPropertyName("sponsoredMerchantData")]
        public object SponsoredMerchantData { get; set; }

        [Required]
        [JsonPropertyName("subsequentTransactionType")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public SubsequentTransactionType SubsequentTransactionType { get; set; }

        [JsonPropertyName("cardVerificationData")]
        public CardVerificationData CardVerificationData { get; set; }

        /// <summary>
        /// Currency for the 0 amount card check.
        /// If not set then the currency "352" ISK will be used to perform the 0 amount card check.
        /// </summary>
        [JsonPropertyName("currency")]
        public string Currency { get; set; }
    }
}
