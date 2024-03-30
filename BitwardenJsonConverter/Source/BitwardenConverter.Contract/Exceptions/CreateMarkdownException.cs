namespace KaWoDev.BitwardenJsonConverter.BitwardenConverter.Contract.Exceptions;

public class CreateMarkdownException : BaseException
{
	public CreateMarkdownException()
	{ }

	public CreateMarkdownException(string message)
		: base(message)
	{ }

	public CreateMarkdownException(string message, Exception inner)
		: base(message, inner)
	{ }
}