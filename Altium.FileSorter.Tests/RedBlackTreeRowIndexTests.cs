namespace Altium.FileSorter.Tests;

public class RedBlackTreeRowIndexTests
{
    [Test]
    public void Should_Iterate_Over_Sorted_Items()
    {
        var sut = new RedBlackTreeRowIndex();

        var rows = new[]
        {
            new Row(1, "d"),
            new Row(1, "b"),
            new Row(1, "e"),
            new Row(1, "a")
        };

        foreach (var row in rows)
        {
            sut.Insert(row);
        }

        var actualRows = sut.ToArray();
        Array.Sort(rows);

        Assert.That(actualRows, Is.EqualTo(rows));
    }
}