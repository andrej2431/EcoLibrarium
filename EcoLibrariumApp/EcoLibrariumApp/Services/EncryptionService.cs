using System.Security.Cryptography;
using System.Text;

namespace EcoLibrariumApp.Services
{
    internal class EncryptionService
    {
        public static string EncryptUsingSha256(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);
                string hashString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

                return hashString;
            }
        }
    }
}
