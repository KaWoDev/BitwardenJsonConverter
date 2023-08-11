using KaWoDev.BitwardenJsonConverter.BitwardenConverter.Contract.DataClasses;

namespace KaWoDev.BitwardenJsonConverter.BitwardenConverter.Contract;

public interface IMarkdownCreater
{
	string CreateMarkdown(Bitwarden bitwarden, DateTime sourceDate);
}