using System.Text;

namespace Altium.FileSorter.Tests;

public class StreamRowTargetTests
{
    [Test]
    public async Task Should_Write_Rows_To_Target()
    {
        using var stream = new MemoryStream();
        await using var streamWriter = new StreamWriter(stream, Const.Const.DefaultEncoding, leaveOpen: true);
        var rowParser = new RowParser();
        var sut = new StreamRowTarget(streamWriter, rowParser);

        var initialRows = new[]
        {
            new Row(1, "d"),
            new Row(1, "b"),
            new Row(1, "e"),
            new Row(1, "a")
        };

        foreach (var row in initialRows)
        {
            await sut.Write(row);
        }
        
        

        await streamWriter.FlushAsync();

        stream.Position = 0;
        using var streamReader = new StreamReader(stream, Const.Const.DefaultEncoding, leaveOpen: true);
        var actualRows = new List<Row>();
        while (true)
        {
            var line = await streamReader.ReadLineAsync();
            if (line == null)
            {
                break;
            }

            actualRows.Add(rowParser.ToRow(line));
        }

        Assert.That(actualRows, Is.EqualTo(initialRows));
    }
}