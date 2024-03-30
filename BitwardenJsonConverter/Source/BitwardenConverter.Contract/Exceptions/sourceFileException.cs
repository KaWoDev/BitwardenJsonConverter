namespace KaWoDev.BitwardenJsonConverter.BitwardenConverter.Contract.Exceptions;

public class SourceFileException : BaseException
{
    public SourceFileException()
    {
    }

    public SourceFileException(string message) : base(message)
    {
    }

    public SourceFileException(string message, Exception inner) : base(message, inner)
    {
    }

}