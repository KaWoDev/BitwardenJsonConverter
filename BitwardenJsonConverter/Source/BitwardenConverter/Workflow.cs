using KaWoDev.BitwardenJsonConverter.BitwardenConverter.Contract;
using KaWoDev.BitwardenJsonConverter.BitwardenConverter.Contract.Exceptions;

namespace KaWoDev.BitwardenJsonConverter.BitwardenConverter;

public class Workflow : IWorkflow
{
	private readonly IBitwardenJsonConverter _converter;
	private readonly IMarkdownCreater _markdownCreater;

	public Workflow(IBitwardenJsonConverter converter, IMarkdownCreater markdownCreater)
	{
		_converter = converter;
		_markdownCreater = markdownCreater;
	}
	
	public async Task CreateMarkdownFileAsync(FileInfo sourceFile, FileInfo destinationFile)
	{
		ArgumentNullException.ThrowIfNull(sourceFile);
		ArgumentNullException.ThrowIfNull(destinationFile);

		try
		{

			var json = await ReadJsonFileAsync(sourceFile);
			var markdown = CreateMarkdown(json, sourceFile.CreationTime);

			await File.WriteAllTextAsync(destinationFile.FullName, markdown);
		}
		catch (Exception e)
			when(e is not BaseException)
		{
			var myEx = new BaseException($"Es ist ein unerwarteter Fehler aufgetretten.",e);
			throw myEx;
		}

	}
	
	private async Task<string> ReadJsonFileAsync(FileInfo sourcefile)
	{
		if (sourcefile.Exists == false)
		{
			throw new SourceFileException($"Datei '{sourcefile.FullName}' zugriff nicht möglich.");
		}

		var result = await File.ReadAllTextAsync(sourcefile.FullName);
		return result;
	}

	private string CreateMarkdown(string json, DateTime sourceDate)
	{
		var bitwarden = _converter.Deserialize(json);
		
		var result = _markdownCreater.CreateMarkdown(bitwarden, sourceDate);
		return result;
	}

}