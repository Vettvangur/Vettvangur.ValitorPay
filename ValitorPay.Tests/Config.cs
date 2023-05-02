using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValitorPay.Tests
{
    static class Config
    {
        //public static readonly Uri apiUrl = new Uri("https://valitorpay.com");
        public static readonly Uri apiUrl = new Uri("https://uat.valitorpay.com");
        //public const string apiKey = "";
        public const string apiKey = "";

        public const string cardNumber = "2223000010246699";
        public const int expirationMonth = 12;
        public const int expirationYear = 2030;
        public const string _cvc = "123";

        public const string terminalId = "";
        public const string agreementNumber = "";
    }
}
