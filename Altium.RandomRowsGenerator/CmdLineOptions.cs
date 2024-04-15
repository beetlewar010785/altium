using CommandLine;

namespace Altium.RandomRowsGenerator;

public class CmdLineOptions
{
    [Option('o', "out", Required = true, HelpText = "Output file name.")]
    public string OutputFileName { get; set; } = null!;

    [Option('s', "size", Required = true, HelpText = "Approximate file size in bytes.")]
    public long FileSizeInBytes { get; set; }

    [Option("min", Required = false, HelpText = "Minimum length of a random string.")]
    // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Global
    public int MinStringLength { get; set; } = 30;

    [Option("max", Required = false, HelpText = "Maximum length of a random string.")]
    public int MaxStringLength { get; set; } = 1024;
}