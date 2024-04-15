namespace Altium.FileSorter;

public interface IRowParser
{
    string FromRow(Row row);
    Row ToRow(string line);
}