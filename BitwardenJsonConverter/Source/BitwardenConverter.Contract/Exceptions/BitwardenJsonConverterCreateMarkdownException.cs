namespace KaWoDev.BitwardenJsonConverter.BitwardenConverter.Contract.Exceptions;

public class BitwardenJsonConverterCreateMarkdownException : BitwardenConverterException
{
	public BitwardenJsonConverterCreateMarkdownException()
	{ }

	public BitwardenJsonConverterCreateMarkdownException(string message)
		: base(message)
	{ }

	public BitwardenJsonConverterCreateMarkdownException(string message, Exception inner)
		: base(message, inner)
	{ }
}