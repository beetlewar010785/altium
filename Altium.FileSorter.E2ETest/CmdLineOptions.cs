using CommandLine;

// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

namespace Altium.FileSorter.E2ETests;

public class CmdLineOptions
{
    [Option('s', "sorter", Required = true, HelpText = "Path to sorter utility.")]
    public string FileSorterPath { get; set; } = null!;

    [Option('g', "gen", Required = true, HelpText = "Random rows generator utility path.")]
    public string RandomRowsGeneratorPath { get; set; } = null!;

    [Option('d', "dir", Required = true, HelpText = "Directory for temporary files.")]
    public string Directory { get; set; } = null!;

    [Option("size", Required = false, HelpText = "Approximate file size in bytes.")]
    public long FileSizeInBytes { get; set; } = 1024 * 1024;
}