using System.Text;

namespace Altium.FileSorter;

public class StreamRowTarget(TextWriter textWriter, IRowParser parser)
    : IRowTarget
{
    public Task Write(Row row)
    {
        var line = parser.FromRow(row);
        return textWriter.WriteLineAsync(line);
    }
}