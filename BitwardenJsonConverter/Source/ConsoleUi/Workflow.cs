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
	
	public void CreateMarkdownFile(string[] args)
	{
		var sourcefile = GetSourceFileFromArguments(args);
		var json = ReadJsonFile(sourcefile);
		var markdown = CreateMarkdown(json, sourcefile.CreationTime);
		
		File.WriteAllText($"{sourcefile.FullName}.md", markdown);
	}
	
	private FileInfo GetSourceFileFromArguments(string[] args)
	{
		if (args is null || args.Length == 0)
		{
			throw new Exception("Bitte geben Sie den Pfad zur Quelldatei als Parameter an.");
		}

		if (args.Length > 1)
		{
			throw new Exception("Falsche Parameteranzahl.");
		}

		var result = new FileInfo(args[1]);
		return result;
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