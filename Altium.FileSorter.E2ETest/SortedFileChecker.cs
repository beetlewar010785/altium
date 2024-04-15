namespace Altium.FileSorter.E2ETests;

public static class SortedFileChecker
{
    public static async Task<bool> Check(string initialFilePath, string sortedFilePath)
    {
        Console.WriteLine("Checking sorted file...");

        await using var initialFile = File.OpenRead(initialFilePath);
        await using var sortedFile = File.OpenRead(sortedFilePath);

        if (initialFile.Length != sortedFile.Length)
        {
            Console.WriteLine("The size of file sorted is different from the size of initial file.");
            return false;
        }

        using var streamReader = new StreamReader(sortedFile, Const.Const.DefaultEncoding, leaveOpen: true);
        var sortedStream = new StreamRowSource(streamReader, new RowParser());
        Row? prevRow = null;
        var i = 0;
        await foreach (var row in sortedStream)
        {
            if (prevRow != null && !row.Equals(prevRow) && row.CompareTo(prevRow) < 0)
            {
                Console.WriteLine($"Row {row} is unsorted.");
                return false;
            }

            i++;
            if (i % Const.Const.LogPeriod == 0)
            {
                Console.WriteLine($"{i} rows have been checked.");
            }

            prevRow = row;
        }

        Console.WriteLine("Sorted file OK.");
        return true;
    }
}