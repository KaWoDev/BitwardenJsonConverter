// See https://aka.ms/new-console-template for more information

using System.CommandLine;
using KaWoDev.BitwardenJsonConverter.BitwardenConverter;

var rootCommand = new RootCommand("""
                                  Bitwarden Json Converter
                                  Convert a JSON Export File from Bitwarden to a Markdown file
                                  """);
var sourcePathArgument = new Argument<FileInfo>("source", """
                                                          Path to the source JSON file.
                                                          This must be en encrypted JSON export file from Bitwarden.
                                                          """);
rootCommand.Add(sourcePathArgument);

var destinationPathArugument = new Argument<FileInfo>("destination", "Save path for the destination file.");
rootCommand.Add(destinationPathArugument);

rootCommand.SetHandler(async (source, destination) =>
{
    var converter = new BitwardenJsonConverter();
    var markdowncreater = new MarkdownCreater();
    var workflow = new Workflow(converter, markdowncreater);

    await workflow.CreateMarkdownFileAsync(source, destination);
},sourcePathArgument, destinationPathArugument);

rootCommand.InvokeAsync(args);


