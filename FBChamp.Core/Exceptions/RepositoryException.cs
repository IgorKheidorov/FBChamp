namespace FBChamp.Core.Exceptions;

[Serializable]
public class RepositoryException : Exception
{
    public List<string> Errors { get; } = [];

    public RepositoryException(string message) : base(message)
    {
    }

    public RepositoryException(string message, Exception innerException) : base(message, innerException)
    {
    }
}