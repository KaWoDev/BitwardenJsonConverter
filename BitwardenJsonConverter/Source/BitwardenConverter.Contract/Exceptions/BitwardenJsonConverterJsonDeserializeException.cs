namespace KaWoDev.BitwardenJsonConverter.BitwardenConverter.Contract.Exceptions;

public class BitwardenJsonConverterJsonDeserializeException : BitwardenConverterException
{
	public BitwardenJsonConverterJsonDeserializeException()
	{ }

	public BitwardenJsonConverterJsonDeserializeException(string message)
		: base(message)
	{ }

	public BitwardenJsonConverterJsonDeserializeException(string message, Exception inner)
		: base(message, inner)
	{ }
}