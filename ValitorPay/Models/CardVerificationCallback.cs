using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace Vettvangur.ValitorPay.Models
{
    public class CardVerificationCallback
    {
        public string Version { get; set; }

        public string MerchantID { get; set; }

        public string Xid { get; set; }

        public int MdStatus { get; set; }

        public string MdErrorMsg { get; set; }

        public string TxStatus { get; set; }

        public string iReqCode { get; set; }

        public string iReqDetail { get; set; }

        public string VendorCode { get; set; }

        public string Eci { get; set; }

        public string Cavv { get; set; }

        public string CavvAlgorithm { get; set; }

        private string _md;
        public string MD 
        {
            get => _md;
            set => _md = value; 
        }

        public byte[] GetMerchantData()
        {
            return Convert.FromBase64String(_md);
        }
        public T GetMerchantData<T>(string secret)
        {
            // ToDo: Might be more useful to use the web server private key and certificate for encrypt/decrypt
            // Possibly more automatic and would save on space needed for IV field
            // Would need testing of how certificate store works on all platforms, Azure/Linux
            // Also consider if we need to pin the checksum here to make sure we use the same cert/key on decrypt
            var mdBytes = AesCryptoHelper.Decrypt(secret, _md);
            MemoryStream stream;
            using (var inputStream = new MemoryStream(mdBytes))
            using (stream = new MemoryStream())
            using (var deflateStream = new DeflateStream(inputStream, CompressionMode.Decompress))
            {
                deflateStream.CopyTo(stream);
            }

            return JsonSerializer.Deserialize<T>(stream.ToArray());
        }

        public string PAResVerified { get; set; }

        public string PAResSyntaxOK { get; set; }

        public string Digest { get; set; }

        public string sID { get; set; }

        public TDS2 TDS2 { get; set; }

        public string Signature { get; set; }
    }

    public class TDS2
    {
        public string TransStatus { get; set; }

        public string ThreeDSServerTransID { get; set; }

        public string DsTransID { get; set; }

        public string AcsTransID { get; set; }

        public string AcsReferenceNumber { get; set; }

        public string AuthTimestamp { get; set; }

        public string MessageVersion { get; set; }

        public string AuthenticationType { get; set; }

        public string AcsOperatorID { get; set; }

        public string AResExtensions { get; set; }
    }

    /// <summary>
    /// Example merchant data object
    /// </summary>
    public class MerchantDataVirtualCard
    {
        public string OrderId { get; set; }

        public string VirtualCard { get; set; }
    }

    /// <summary>
    /// Example merchant data object
    /// </summary>
    public class MerchantDataCard
    {
        public string OrderId { get; set; }

        public string CardNumber { get; set; }

        public int ExpirationMonth { get; set; }

        public int ExpirationYear { get; set; }

        public string Cvc { get; set; }
    }
}
