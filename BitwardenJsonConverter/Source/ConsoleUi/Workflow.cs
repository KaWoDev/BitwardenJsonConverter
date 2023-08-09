using KaWoDev.BitwardenJsonConverter.BitwardenConverter;

namespace KaWoDev.BitwardenJsonConverter.ConsoleUi;

public class Workflow
{
	public string ReadJsonFile(string path)
	{
		if (string.IsNullOrWhiteSpace(path))
		{
			throw new Exception($"Pfad '{path}' ist nicht gültig.");
		}

		var file = new FileInfo(path);
		if (file.Exists == false)
		{
			throw new Exception($"Datei '{file.FullName}' zugriff nicht möglich.");
		}

		var fileContent = File.ReadAllText(file.FullName);

		return fileContent;
	}

	public FileInfo GetSourceFileFromArguments(string[] args)
	{
		if (args is null || args.Length == 0)
		{
			throw new Exception("Bitte geben Sie den Pfad zur Quelldatei als Parameter an.");
		}

		if (args.Length > 1)
		{
			throw new Exception("Falsche Parameteranzahl.");
		}

		var sourcefile = new FileInfo(args[1]);
		if (sourcefile.Exists)
		{
			throw new Exception($"Auf die Quelldatei '{sourcefile.FullName}' kann nicht zugegriffen werden.");
		}

		return sourcefile;
	}

	public void CreateMarkdownFile(FileInfo sourcefile)
	{
		var json = new Workflow().ReadJsonFile(sourcefile.FullName);
		
		var converter = new BitwardenConverter.BitwardenJsonConverter();
		var bitwarden = converter.Deserialize(json);
		
		var md = new MarkdownCreater().CreateMarkdown(bitwarden, sourcefile.CreationTime);

		File.WriteAllText($"{sourcefile.FullName}.md", md);
		Console.WriteLine($"Markdown Datei '{sourcefile.FullName}.md' erstellt.");
	}
}