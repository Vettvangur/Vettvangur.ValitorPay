using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Vettvangur.ValitorPay.Models
{
    public class FirstTransactionData
    {
        [JsonPropertyName("initiationReason")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public InitiationReason InitiationReason { get; set; }
    }

    public enum InitiationReason
    {
        CredentialOnFile,

        Recurring,

        Installment,
    }
}
