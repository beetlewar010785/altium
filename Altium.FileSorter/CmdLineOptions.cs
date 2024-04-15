using CommandLine;

// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

namespace Altium.FileSorter;

public class CmdLineOptions
{
    [Option('i', "in", Required = true, HelpText = "Input file name.")]
    public string InputFileName { get; set; } = null!;

    [Option('o', "out", Required = true, HelpText = "Output file name.")]
    public string OutputFileName { get; set; } = null!;
}