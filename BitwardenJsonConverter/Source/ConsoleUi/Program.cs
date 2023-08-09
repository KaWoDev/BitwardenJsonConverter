// See https://aka.ms/new-console-template for more information

using KaWoDev.BitwardenJsonConverter.ConsoleUi;

var workflow = new Workflow();
var sourcefile = workflow.GetSourceFileFromArguments(args);

workflow.CreateMarkdownFile(sourcefile);