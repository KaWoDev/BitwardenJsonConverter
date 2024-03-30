namespace KaWoDev.BitwardenJsonConverter.BitwardenConverter.Contract;

public interface IWorkflow
{
    Task CreateMarkdownFileAsync(FileInfo sourceFile, FileInfo destinationFile);
}