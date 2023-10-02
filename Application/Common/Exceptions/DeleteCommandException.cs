namespace Application.Common.Exceptions;

public class DeleteCommandException : Exception
{
    public DeleteCommandException()
    {
    }

    public DeleteCommandException(string message)
        : base(message)
    {
    }

    public DeleteCommandException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public DeleteCommandException(string name, object key)
        : base($"Entity '{name}' ({key}) was not DELETE.")
    {
    }
}
