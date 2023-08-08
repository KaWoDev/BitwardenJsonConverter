using KaWoDev.BitwardenJsonConverter.Base.Contract.Exceptions;

namespace KaWoDev.BitwardenJsonConverter.BitwardenConverter.Contract.Exceptions;

public class BitwardenConverterException : BaseException
{
	public BitwardenConverterException()
	{ }

	public BitwardenConverterException(string message)
		: base(message)
	{ }

	public BitwardenConverterException(string message, Exception inner)
		: base(message, inner)
	{ }
}