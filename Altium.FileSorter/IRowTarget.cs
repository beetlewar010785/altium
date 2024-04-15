namespace Altium.FileSorter;

public interface IRowTarget
{
    Task Write(Row row);
}