// See https://aka.ms/new-console-template for more information

using KaWoDev.BitwardenJsonConverter.BitwardenConverter;
using KaWoDev.BitwardenJsonConverter.ConsoleUi;

var converter = new BitwardenJsonConverter();
var markdowncreater = new MarkdownCreater();
var workflow = new Workflow(converter, markdowncreater);
workflow.CreateMarkdownFile(args);
