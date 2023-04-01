using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Vettvangur.ValitorPay
{
    public class CardVerificationData
    {
        [JsonPropertyName("mdStatus")]
        public long? MdStatus { get; set; }

        [JsonPropertyName("cavv")]
        public string Cavv { get; set; }

        [JsonPropertyName("xid")]
        public string Xid { get; set; }

        [JsonPropertyName("dsTransId")]
        public string DsTransId { get; set; }

        [JsonPropertyName("eci")]
        public string Eci { get; set; }
    }
}
