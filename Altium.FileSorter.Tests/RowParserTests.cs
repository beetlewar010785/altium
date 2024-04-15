namespace Altium.FileSorter.Tests;

public class RowParserTests
{
    [Test]
    public void Should_Parse_Row_And_Vise_Versa()
    {
        var sut = new RowParser();

        const string initialLine = "1. apple";
        var row = sut.ToRow(initialLine);
        var actualLine = sut.FromRow(row);

        Assert.That(actualLine, Is.EqualTo(initialLine));
    }
}