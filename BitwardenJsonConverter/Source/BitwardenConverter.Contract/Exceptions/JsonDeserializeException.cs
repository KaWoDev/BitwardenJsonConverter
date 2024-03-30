namespace KaWoDev.BitwardenJsonConverter.BitwardenConverter.Contract.Exceptions;

public class JsonDeserializeException : BaseException
{
	public JsonDeserializeException()
	{ }

	public JsonDeserializeException(string message)
		: base(message)
	{ }

	public JsonDeserializeException(string message, Exception inner)
		: base(message, inner)
	{ }
}