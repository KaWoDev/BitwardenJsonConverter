using KaWoDev.BitwardenJsonConverter.BitwardenConverter.Contract;

namespace KaWoDev.BitwardenJsonConverter.ConsoleUi;

public class Workflow
{
	private readonly IBitwardenJsonConverter _converter;
	private readonly IMarkdownCreater _markdownCreater;

	public Workflow(IBitwardenJsonConverter converter, IMarkdownCreater markdownCreater)
	{
		_converter = converter;
		_markdownCreater = markdownCreater;
	}
	
	public void CreateMarkdownFile(FileInfo source, FileInfo destination)
	{
		// if (string.IsNullOrWhiteSpace(source))
		// {
		// 	throw new ArgumentException(nameof(source));
		// }
		//
		// if (string.IsNullOrWhiteSpace(destination))
		// {
		// 	throw new ArgumentException(nameof(destination));
		// }
		
		// var sourcefile = new FileInfo(source);
		var json = ReadJsonFile(source);
		var markdown = CreateMarkdown(json, source.CreationTime);
		
		File.WriteAllText(destination.FullName, markdown);
	}
	
	private string ReadJsonFile(FileInfo sourcefile)
	{
		if (sourcefile.Exists == false)
		{
			throw new Exception($"Datei '{sourcefile.FullName}' zugriff nicht möglich.");
		}

		var result = File.ReadAllText(sourcefile.FullName);
		return result;
	}

	private string CreateMarkdown(string json, DateTime sourceDate)
	{
		var bitwarden = _converter.Deserialize(json);
		
		var result = _markdownCreater.CreateMarkdown(bitwarden, sourceDate);
		return result;
	}

}