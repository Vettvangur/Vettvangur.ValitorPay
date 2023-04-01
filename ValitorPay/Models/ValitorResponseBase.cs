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
    public class ValitorResponseBase
    {
        [JsonPropertyName("isSuccess")]
        public bool IsSuccess { get; set; }

        /// <summary>
        /// https://valitorpay.com/ResponseCodes.html
        /// </summary>
        [JsonPropertyName("responseCode")]
        public string ResponseCode { get; set; }

        [JsonPropertyName("responseDescription")]
        public string ResponseDescription { get; set; }

        /// <summary>
        /// Is a TimeSpan but System.Text.Json won't behave
        /// </summary>
        [JsonPropertyName("responseTime")]
        public string ResponseTime { get; set; }
    }
}
