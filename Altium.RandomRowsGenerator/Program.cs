using Altium.Const;
using Altium.RandomRowsGenerator;
using CommandLine;
using CmdLineOptions = Altium.RandomRowsGenerator.CmdLineOptions;

var result = Parser.Default.ParseArguments<CmdLineOptions>(args);
if (result.Errors.Any())
{
    return -1;
}

var randomGenerator = new LineGenerator(
    1,
    10000,
    result.Value.MinStringLength,
    result.Value.MaxStringLength);

Console.WriteLine(
    $"Generating file {result.Value.OutputFileName} with strings of length from {result.Value.MinStringLength} to {result.Value.MaxStringLength} and size {result.Value.FileSizeInBytes}...");

using var file = File.Open(result.Value.OutputFileName, FileMode.Create);
var i = 0;
const int repeatPeriod = 1000;
while (true)
{
    var row = $"{randomGenerator.GenerateInt()}. {randomGenerator.GenerateString()}{Environment.NewLine}";
    file.Write(Const.DefaultEncoding.GetBytes(row));
    i++;

    if (i % repeatPeriod == 0)
    {
        file.Write(Const.DefaultEncoding.GetBytes(row));
        i++;
    }

    if (i % Const.LogPeriod == 0)
    {
        Console.WriteLine($"Written rows: {i}.");
    }

    if (file.Length >= result.Value.FileSizeInBytes)
    {
        break;
    }
}

Console.WriteLine("File successfully generated.");

return 0;