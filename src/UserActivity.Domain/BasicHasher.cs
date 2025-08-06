using System.Security.Cryptography;
using System.Text;

namespace UserActivity.Domain;

public static class BasicHasher
{
    private const string Salt = "BasicSalt";

    public static string Hash(string rawPassword)
    {
        byte[] encodedRawPassword = Encoding.UTF8.GetBytes(rawPassword + Salt);
        byte[] hashedData = SHA256.HashData(encodedRawPassword);
        return Convert.ToBase64String(hashedData);
    }
}
