using System.Collections;
using Towel.DataStructures;

namespace Altium.FileSorter;

public class RedBlackTreeRowIndex : IRowIndex
{
    private readonly IRedBlackTree<Row> _tree = RedBlackTreeLinked.New<Row>();

    public void Insert(Row row)
    {
        _tree.Add(row);
    }

    public IEnumerator<Row> GetEnumerator()
    {
        return _tree.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}