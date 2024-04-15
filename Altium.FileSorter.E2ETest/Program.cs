using System.Diagnostics;
using Altium.FileSorter.E2ETests;
using CommandLine;

var result = Parser.Default.ParseArguments<CmdLineOptions>(args);
if (result.Errors.Any())
{
    return -1;
}

var suite = DateTime.Now.ToString("s").Replace(":", "").Replace("-", "");

Console.WriteLine($"Test suite: {suite}");

var generatingFilePath = Path.Join(result.Value.Directory, $"{suite}-in.txt");
var randomFileProcess = Process.Start(
    result.Value.RandomRowsGeneratorPath,
    new[]
    {
        "-o", generatingFilePath,
        "-s", $"{result.Value.FileSizeInBytes}"
    });
await randomFileProcess.WaitForExitAsync();
if (randomFileProcess.ExitCode != 0)
{
    return randomFileProcess.ExitCode;
}

var sortingFilePath = Path.Join(result.Value.Directory, $"{suite}-out.txt");
var sortingProcess = Process.Start(
    result.Value.FileSorterPath,
    new[]
    {
        "-i", generatingFilePath,
        "-o", sortingFilePath,
    });
await sortingProcess.WaitForExitAsync();
if (sortingProcess.ExitCode != 0)
{
    return sortingProcess.ExitCode;
}

var ok = await SortedFileChecker.Check(generatingFilePath, sortingFilePath);
if (!ok)
{
    return -1;
}

return 0;