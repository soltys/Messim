using System;
using System.Security.Cryptography;
using System.Text;

namespace Messim.Util
{
    /// <summary>
    /// Static class with methods to create SHA hash strings
    /// </summary>
    public static class SHA
    {
        /// <summary>
        /// Creates SHA-1 for text
        /// </summary>
        /// <param name="text">Input text from which is created SHA1</param>
        /// <param name="encoding">Encoding for text</param>
        /// <returns>Cryptografic hash SHA-1</returns>
        public static string CreateSHA1Hash(string text, Encoding encoding)
        {
            byte[] buffer = encoding.GetBytes(text);
            var cryptoTransformSHA1 =
            new SHA1CryptoServiceProvider();
            string hash = BitConverter.ToString(
                cryptoTransformSHA1.ComputeHash(buffer)).Replace("-", "");

            return hash;
        }

        /// <summary>
        /// Creates SHA-1 for text, encoding for text is Encoding.Default
        /// </summary>
        /// <param name="text">Input text from which is created SHA1</param>
        /// <returns>Cryptografic hash SHA-1</returns>
        public static string CreateSHA1Hash(string text)
        {
            return CreateSHA1Hash(text, Encoding.Default);
        }

        /// <summary>
        /// Creates SHA-512 for text
        /// </summary>
        /// <param name="text">Input text from which is created SHA1</param>
        /// <param name="encoding">Encoding for text</param>
        /// <returns>Cryptografic hash SHA-512</returns>
        public static string CreateSHA512Hash(string text, Encoding encoding)
        {
            byte[] buffer = encoding.GetBytes(text);
            var cryptoTransformSHA512 = new SHA512CryptoServiceProvider();

            string hash = BitConverter.ToString(
                cryptoTransformSHA512.ComputeHash(buffer)).Replace("-", "");

            return hash;
        }

        public static string CreateSHA512HashToBase64String(string text, Encoding encoding)
        {
            byte[] buffer = encoding.GetBytes(text);
            var cryptoTransformSHA512 = new SHA512CryptoServiceProvider();

            string hash = Convert.ToBase64String(
                cryptoTransformSHA512.ComputeHash(buffer)).Replace("-", "");

            return hash;
        }
        /// <summary>
        /// Creates SHA-512 for text, encoding for text is Encoding.Default
        /// </summary>
        /// <param name="text">Input text from which is created SHA1</param>
        /// <returns>Cryptografic hash SHA-512</returns>
        public static string CreateSHA512Hash(string text)
        {
            return CreateSHA512Hash(text, Encoding.Default);
        }

        /// <summary>
        /// Creates SHA-256 for text
        /// </summary>
        /// <param name="text">Input text from which is created SHA1</param>
        /// <param name="encoding">Encoding for text</param>
        /// <returns>Cryptografic hash SHA-256</returns>
        public static string CreateSHA256Hash(string text, Encoding encoding)
        {
            byte[] buffer = encoding.GetBytes(text);
            var cryptoTransformSHA512 = new SHA256CryptoServiceProvider();

            string hash = BitConverter.ToString(
                cryptoTransformSHA512.ComputeHash(buffer)).Replace("-", "");

            return hash;
        }

        /// <summary>
        /// Creates SHA-256 for text, encoding for text is Encoding.Default
        /// </summary>
        /// <param name="text">Input text from which is created SHA1</param>
        /// <returns>Cryptografic hash SHA-256</returns>
        public static string CreateSHA256Hash(string text)
        {
            return CreateSHA256Hash(text, Encoding.Default);
        }

    }
}
