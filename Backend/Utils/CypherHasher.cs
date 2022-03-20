using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CodingDays.Utils;
public static class CypherHasher
{
    public static string HashCypherResult(string cypherResult)
    {
        string changedString = string.Join(".", cypherResult.ToArray());
        byte[] resultBytes = Encoding.UTF8.GetBytes(changedString);
        byte[] hashedCypherResult = SHA1.HashData(resultBytes);
        string hash = string.Join("", hashedCypherResult.Select(b => b.ToString("x").PadLeft(2, '0')));

        return hash;
    }
}
