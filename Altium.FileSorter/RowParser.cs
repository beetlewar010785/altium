namespace Altium.FileSorter;

public class RowParser : IRowParser
{
    public string FromRow(Row row)
    {
        return $"{row.Left}. {row.Right}";
    }

    public Row ToRow(string line)
    {
        var parts = line.Split(". ");
        if (parts.Length != 2)
        {
            throw new Exception("Unexpected row");
        }

        return new Row(int.Parse(parts[0]), parts[1]);
    }
}