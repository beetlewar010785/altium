// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using Altium.Const;
using Altium.FileSorter;
using CommandLine;

var result = Parser.Default.ParseArguments<CmdLineOptions>(args);
if (result.Errors.Any())
{
    return -1;
}

Console.WriteLine($"Opening input file: {result.Value.InputFileName}.");
await using var inputFile = File.OpenRead(result.Value.InputFileName);

Console.WriteLine($"Creating output file: {result.Value.OutputFileName}.");
await using var outputFile = File.Create(result.Value.OutputFileName);

var rowSerializer = new RowParser();

using var inputFileReader = new StreamReader(inputFile, Const.DefaultEncoding, leaveOpen: true);

var rowSource = new StreamRowSource(inputFileReader, rowSerializer);

var rowIndex = new RedBlackTreeRowIndex();

await using var outputFileWriter = new StreamWriter(outputFile, Const.DefaultEncoding, leaveOpen: true);

var rowTarget = new StreamRowTarget(outputFileWriter, rowSerializer);

var sorter = new Sorter(rowSource, rowIndex, rowTarget);

Console.WriteLine("Start sorting...");
var sw = new Stopwatch();
sw.Start();
await sorter.Sort();
sw.Stop();
Console.WriteLine($"Sorting completed for {sw.Elapsed}.");

await outputFileWriter.FlushAsync();

return 0;