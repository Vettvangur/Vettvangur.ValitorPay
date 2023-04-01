using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Vettvangur.ValitorPay.Models
{
    public class AdditionalData
    {
        static readonly Regex referenceDataRegex = new Regex("^[a-zA-Z0-9]*$");

        [JsonPropertyName("airlineData")]
        public object AirlineData { get; set; }

        private string _merchantReferenceData;
        [JsonPropertyName("merchantReferenceData")]
        public string MerchantReferenceData 
        {
            get => _merchantReferenceData;
            set
            {
                if (!referenceDataRegex.IsMatch(value ?? ""))
                {
                    throw new ArgumentException("'Merchant Reference Data' can only contain characters matching [a-zA-Z0-9]");
                }

                _merchantReferenceData = value;
            }
        }

        [JsonPropertyName("tags")]
        public string Tags { get; set; }
    }

    public class CardPaymentAdditionalData : AdditionalData
    {
        [JsonPropertyName("dccData")]
        public string DccData { get; set; }
    }
}
