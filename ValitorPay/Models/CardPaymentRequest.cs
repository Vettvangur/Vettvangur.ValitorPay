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
    public class CardPaymentRequest : ValitorRequestBase
    {
        [JsonPropertyName("operation")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Operation Operation { get; set; } = Operation.Sale;

        [JsonPropertyName("authType")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public AuthType? AuthType { get; set; }

        [JsonPropertyName("transactionType")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TransactionType TransactionType { get; set; } = TransactionType.ECommerce;

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

        [JsonPropertyName("cvc")]
        public string CVC { get; set; }

        [JsonPropertyName("firstTransactionData")]
        public FirstTransactionData FirstTransactionData { get; set; }

        [JsonPropertyName("subsequentTransactionData")]
        public object SubsequentTransactionData { get; set; }

        [JsonPropertyName("cardVerificationData")]
        public CardVerificationData CardVerificationData { get; set; }

        [JsonPropertyName("additionalData")]
        public CardPaymentAdditionalData AdditionalData { get; set; }

        [JsonPropertyName("maskedCardNumber")]
        public string MaskedCardNumber { get; set; }

        [JsonPropertyName("walletData")]
        public object WalletData { get; set; }

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

        [JsonPropertyName("delayedClearingData")]
        public string DelayedClearingData { get; set; }

        [JsonPropertyName("isFinalCapture")]
        public bool IsFinalCapture { get; set; }

        [JsonPropertyName("scaExemption")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ScaExemption ScaExemption { get; set; }
    }
}
