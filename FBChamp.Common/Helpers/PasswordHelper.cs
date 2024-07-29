using System.Security.Cryptography;
using System.Text;
using FBChamp.Common.Cryptography;

namespace FBChamp.Common.Helpers;

public static class PasswordHelper
{
    /// <exception cref="System.ArgumentOutOfRangeException"><paramref name="length" /> must be > 0.</exception>
    public static string GenerateSalt(int length)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(length);

        var randomBytes = RandomNumberGenerator.GetBytes(length);
        return Encoding.Unicode.GetString(randomBytes);
    }

    /// <exception cref="System.ArgumentNullException"></exception>
    public static string ComputeHash(string password, string salt)
    {
        ArgumentNullException.ThrowIfNull(password);
        ArgumentNullException.ThrowIfNull(salt);

        var hashAlgorithm = new Sha512Hash();
        return hashAlgorithm.CalculateHash(password + salt);
    }

    /// <exception cref="System.ArgumentOutOfRangeException"><paramref name="length" /> must be > 0.</exception>
    public static string Generate(int length)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(length);

        var randomString = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Trim();
        return length <= randomString.Length ? randomString[..length] : randomString;
    }
}