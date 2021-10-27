using System.Security.Cryptography;
using System.Text;

namespace WebApplication1.Codes
{
    public class HashingExample
    {
        public string MD5Hash(string valueToHash)
        {
            byte[] valueAsBytes = ASCIIEncoding.ASCII.GetBytes(valueToHash);

            byte[] HashedMD5 = MD5.HashData(valueAsBytes);

            string hashedValueAsString = Convert.ToBase64String(HashedMD5);

            return hashedValueAsString;
            //return "Alexander Roikier Møller";
        }
        public string BcryptHash(string valueToHash)
        {
            string hashed = BCrypt.Net.BCrypt.HashPassword(valueToHash, BCrypt.Net.SaltRevision.Revision2Y);
            return hashed;
            //return "Møller Roikier Alexander";
        }
    }
}
