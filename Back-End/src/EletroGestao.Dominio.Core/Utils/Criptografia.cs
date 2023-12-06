using System.Security.Cryptography;
using System.Text;

namespace EletroGestao.Dominio.Core.Utils
{
    public static class Criptografia
    {
        /// <summary>
        /// Returns the hash of the given string. 
        /// </summary>
        /// <param name="stringToHash" />string for which the hash should be generated
        /// <param name="hashAlgorithm" />Hash algorithm. Ex: MD5, SHA1, SHA256, SHA384, SHA512
        /// <returns></returns>
        public static string GetHash(this string stringToHash, string hashAlgorithm)
        {
            var algorithm = HashAlgorithm.Create(hashAlgorithm);
            byte[] hash = algorithm.ComputeHash(System.Text.Encoding.UTF8.GetBytes(stringToHash));

            // ToString("x2")  converts byte in hexadecimal value
            string encryptedVal = string.Concat(hash.Select(b => b.ToString("x2"))).ToUpperInvariant();
            return encryptedVal;
        }

        /// <summary>
        /// Criptografa string
        /// </summary>
        /// <param name="Texto">Texto a ser criptgrafado</param>
        /// <returns>Valor criptografado</returns>
        public static string Criptografa(this string Texto)
        {
            return Convert.ToBase64String(ASCIIEncoding.UTF8.GetBytes(Texto));
        }
    }
}
