namespace Altium.FileSorter;

public interface IRowIndex : IEnumerable<Row>
{
    void Insert(Row row);
}