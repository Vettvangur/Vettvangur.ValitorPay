using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Vettvangur.ValitorPay
{
    /// <summary>
    /// Helps with AES crypto calculations
    /// </summary>
    public static class AesCryptoHelper
    {
        /// <summary>
        /// Encrypt chosen input with the provided base64 encoded secret key.
        /// Returns a string of format {IV} {cipherText}
        /// </summary>
        /// <param name="secret">Base64 encoded 64/128/256bit encryption key</param>
        /// <param name="inputBytes"></param>
        /// <returns>string of format {IV} {cipherText}</returns>
        public static string Encrypt(string secret, byte[] inputBytes)
        {
            if (inputBytes == null || inputBytes.Length == 0)
                throw new ArgumentNullException(nameof(inputBytes));

            byte[] encryptionKey = ValidateAndParseKey(secret);

            using (Aes aes = Aes.Create())
            {
#pragma warning disable CA5401 // Aes.create() generates new IV each time, verified.
                // also https://stackoverflow.com/a/52707014/5663961
                ICryptoTransform encryptor = aes.CreateEncryptor(encryptionKey, aes.IV);
#pragma warning restore CA5401 // Do not use CreateEncryptor with non-default IV

                byte[] encryptedBytes = PerformCryptography(encryptor, inputBytes);

                return Convert.ToBase64String(aes.IV.Concat(encryptedBytes).ToArray());
            }
        }

        /// <summary>
        /// Decrypt chosen input with the provided base64 encoded secret key.
        /// Takes a string of format {IV} {cipherText}
        /// </summary>
        /// <param name="secret">Base64 encoded 64/128/256bit encryption key</param>
        /// <param name="input">string of format {IV} {cipherText}</param>
        /// <returns>Plaintext</returns>
        public static byte[] Decrypt(string secret, string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentNullException(nameof(input));

            byte[] encryptionKey = ValidateAndParseKey(secret);

            var bytes = Convert.FromBase64String(input);
            var iv = bytes.Take(16).ToArray();
            using (Aes aes = Aes.Create())
            {
                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor 
                    = aes.CreateDecryptor(encryptionKey, iv);

                return PerformCryptography(
                    decryptor, 
                    bytes.Skip(16).ToArray());
            }
        }

        private static byte[] ValidateAndParseKey(string secret)
        {
            if (string.IsNullOrEmpty(secret))
                throw new ArgumentNullException(nameof(secret));

            byte[] encryptionKey = Convert.FromBase64String(secret);

            if (encryptionKey.Length != 8
            && encryptionKey.Length != 16
            && encryptionKey.Length != 32)
                throw new ArgumentException("Encryption key must be 64/128/256bit", nameof(secret));

            return encryptionKey;
        }

        private static byte[] PerformCryptography(ICryptoTransform cryptoTransform, byte[] data)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(data, 0, data.Length);
                    cryptoStream.FlushFinalBlock();
                    return memoryStream.ToArray();
                }
            }
        }
    }
}
