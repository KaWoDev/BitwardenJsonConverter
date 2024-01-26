// See https://aka.ms/new-console-template for more information

using System.CommandLine;
using KaWoDev.BitwardenJsonConverter.BitwardenConverter;
using KaWoDev.BitwardenJsonConverter.BitwardenConverter.Contract;
using KaWoDev.BitwardenJsonConverter.ConsoleUi;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddTransient<IBitwardenJsonConverter,BitwardenJsonConverter>();
        services.AddTransient<IMarkdownCreater,MarkdownCreater>();

        services.AddTransient<Workflow>();
    })
    .Build();

var workflow = host.Services.GetRequiredService<Workflow>();

//Todo Only for test
args = new[] {  "sd", "dsa"};

var rootCommand = new RootCommand("Bitwarden JSON Converter Application");
var sourcePathArgument= new Argument<FileInfo>("source", "The path from the Bitwarden JSON export file.");
var destinationPathArgument= new Argument<FileInfo>("destination", "The destination path for the output file.");

rootCommand.Add(sourcePathArgument);
rootCommand.Add(destinationPathArgument);
rootCommand.SetHandler((sourcePath, destinationPath) =>
{
    var workflow = host.Services.GetRequiredService<Workflow>();
    // workflow.CreateMarkdownFile(ctx.BindingContext.ParseResult.Tokens[0].Value,ctx.BindingContext.ParseResult.Tokens[1].Value );
    workflow.CreateMarkdownFile(sourcePath, destinationPath);
},sourcePathArgument, destinationPathArgument);

rootCommand.Invoke(args);