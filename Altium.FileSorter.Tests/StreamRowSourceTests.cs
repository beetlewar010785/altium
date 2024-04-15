using System.Text;

namespace Altium.FileSorter.Tests;

public class StreamRowSourceTests
{
    [Test]
    public async Task Should_Iterate_Over_Rows()
    {
        var rows = new[]
        {
            new Row(1, "d"),
            new Row(1, "b"),
            new Row(1, "e"),
            new Row(1, "a")
        };

        using var stream = new MemoryStream();
        using var streamReader = new StreamReader(stream, Const.Const.DefaultEncoding, leaveOpen: true);
        foreach (var row in rows)
        {
            await stream.WriteAsync(Const.Const.DefaultEncoding.GetBytes($"{row.Left}. {row.Right}{Environment.NewLine}"));
        }

        stream.Position = 0;

        var sut = new StreamRowSource(streamReader, new RowParser());
        var actualRows = sut.ToBlockingEnumerable().ToArray();

        Assert.That(actualRows, Is.EqualTo(rows));
    }

    [Test]
    public void Should_Ignore_Last_Line_Ended_With_New_Line_Symbol()
    {
        var incomingString =
            $"1. a{Environment.NewLine}" +
            $"2. b{Environment.NewLine}";

        using var stream = new MemoryStream(Const.Const.DefaultEncoding.GetBytes(incomingString));
        using var streamReader = new StreamReader(stream, Const.Const.DefaultEncoding, leaveOpen: true);

        var sut = new StreamRowSource(streamReader, new RowParser());
        var actualRows = sut.ToBlockingEnumerable().ToArray();

        Assert.That(actualRows, Has.Length.EqualTo(2));
    }
}