namespace FBChamp.Core.Exceptions;

[Serializable]
public class RepositoryException : Exception
{
    public List<string> Errors { get; } = new List<string>();

    public RepositoryException(string message) 
        : base(message)
    {
    }

    public RepositoryException(string message, Exception innerException) 
        : base(message, innerException)
    {
    }
}