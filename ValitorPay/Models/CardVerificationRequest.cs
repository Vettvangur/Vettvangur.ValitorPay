using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;

namespace Vettvangur.ValitorPay
{
    public class CardVerificationRequest : ValitorRequestBase
    {
        /// <summary>
        /// Either provide virtual card in new or old format, or cardNumber w/ expiration
        /// </summary>
        [JsonPropertyName("virtualCard")]
        public string VirtualCard { get; set; }

        /// <summary>
        /// Either provide virtual card or cardNumber w/ expiration
        /// </summary>
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

        /// <summary>
        /// In most cases this value should be 'WWW' (WAP and DTV are obsolete), 
        /// unless explicitly requesting 3DSecure v2.0 authentication.
        /// </summary>
        [JsonPropertyName("cardholderDeviceType")]
        public string CardholderDeviceType { get; set; } = "WWW";

        [JsonPropertyName("amount")]
        public long Amount { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; } = "ISK";

        [JsonPropertyName("authenticationUrl")]
        public Uri AuthenticationUrl { get; set; }

        // not support in net50
        //public void SetRelativeAuthenticationUrl(HttpContextBase httpContext, Uri uri)
        //{
        //    if (httpContext == null)
        //    {
        //        throw new ArgumentNullException(nameof(httpContext));
        //    }
        //    if (httpContext.Request == null)
        //    {
        //        throw new ArgumentNullException(nameof(httpContext.Request));
        //    }
        //    if (uri == null)
        //    {
        //        throw new ArgumentNullException(nameof(uri));
        //    }
        //    if (uri.IsAbsoluteUri)
        //    {
        //        throw new ArgumentException("Please provide a relative uri", nameof(uri));
        //    }

        //    AuthenticationUrl 
        //        = $"{httpContext.Request.Url.Scheme}://{httpContext.Request.Url.Authority}"
        //        + uri.ToString();
        //}

        [JsonPropertyName("authenticationSuccessUrl")]
        public Uri AuthenticationSuccessUrl { get; set; }

        [JsonPropertyName("authenticationFailedUrl")]
        public Uri AuthenticationFailedUrl { get; set; }

        private string _md;
        [JsonPropertyName("md")]
        public string Md => _md;

        /// <summary>
        /// Holds merchant specific data that is attached to the request. <br />
        /// This field is optional and can be used for handling merchant session state during 
        /// 3D secure authentication.  <br />
        /// The field is limited to a maximum of 254 characters after base64 encoding. <br />
        /// <br />
        /// Base64 encoding handled here.
        /// </summary>
        /// <exception cref="ArgumentException">The field is limited to a maximum of 254 characters after base64 encoding.</exception>
        public void SetMerchantData(object merchantData, string secret)
        {
            var json = JsonSerializer.Serialize(merchantData);
            var jsonBytes = Encoding.UTF8.GetBytes(json);

            MemoryStream stream;
            using (stream = new MemoryStream())
            using (var deflateStream = new DeflateStream(stream, CompressionMode.Compress))
            {
                deflateStream.Write(jsonBytes, 0, jsonBytes.Length);
            }

            // ToDo: Might be more useful to use the web server private key and certificate for encrypt/decrypt
            // Possibly more automatic and would save on space needed for IV field
            var md = AesCryptoHelper.Encrypt(secret, stream.ToArray());

            if (md.Length > 254)
            {
                throw new ArgumentException("The field is limited to a maximum of 254 characters after base64 encoding.");
            }

            _md = md;
        }

        /// <summary>
        /// Holds merchant specific data that is attached to the request. <br />
        /// This field is optional and can be used for handling merchant session state during 
        /// 3D secure authentication.  <br />
        /// Use either merchantDataBytes or MerchantData. <br />
        /// <br />
        /// Base64 encoding handled here.
        /// </summary>
        /// <exception cref="ArgumentException">The field is limited to a value of maximum of 254 characters after base64 encoding.</exception>
        [JsonIgnore]
        public byte[] MerchantDataBytes
        {
            set
            {
                var md = Convert.ToBase64String(value);
                if (md.Length > 254)
                {
                    throw new ArgumentException("The field is limited to a maximum of 254 characters after base64 encoding.");
                }

                _md = md;
            }
        }

        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        [JsonPropertyName("exponent")]
        public string Exponent { get; set; } = "0";

        [JsonPropertyName("threeDs20AdditionalParameters")]
        public ThreeDs20AdditionalParamaters ThreeDs20AdditionalParamaters { get; set; }
    }

    public class ThreeDs20AdditionalParamaters
    {
        [JsonPropertyName("threeDs2XGeneralExtrafields")]
        public ThreeDs2XGeneralExtrafields ThreeDs2XGeneralExtrafields { get; set; }
    }

    public class ThreeDs2XGeneralExtrafields
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("threeDsRequestorChallengeInd")]
        public ThreeDsRequestorChallenge? ThreeDsRequestorChallengeInd { get; set; }
    }

    public enum ThreeDsRequestorChallenge
    {
        ChallengeRequested_Mandate
    }
}
