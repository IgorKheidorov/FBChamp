namespace FBChamp.Common.Cryptography;

public abstract class HashAlgorithm : IHashAlgorithm
{
    public string CalculateHash(string text)
    {
        ArgumentNullException.ThrowIfNull(text);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(text.Length);

        return CalculateHashInternal(text);
    }

    protected abstract string CalculateHashInternal(string text);
}