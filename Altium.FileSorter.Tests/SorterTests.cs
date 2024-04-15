using System.Text;

namespace Altium.FileSorter.Tests;

public class SorterTests
{
    [TestCaseSource(nameof(GetTestCases))]
    public async Task<string> Should_Sort_Rows(string initialString)
    {
        var rowParser = new RowParser();

        var initialBytes = Const.Const.DefaultEncoding.GetBytes(initialString);
        using var sourceStream = new MemoryStream(initialBytes);
        using var sourceStreamReader = new StreamReader(sourceStream, Const.Const.DefaultEncoding, leaveOpen: true);
        var rowSource = new StreamRowSource(sourceStreamReader, rowParser);

        var rowIndex = new RedBlackTreeRowIndex();

        using var targetStream = new MemoryStream();
        await using var targetStreamWriter =
            new StreamWriter(targetStream, Const.Const.DefaultEncoding, leaveOpen: true);
        var rowTarget = new StreamRowTarget(targetStreamWriter, rowParser);

        var sut = new Sorter(rowSource, rowIndex, rowTarget);

        await sut.Sort();
        await targetStreamWriter.FlushAsync();

        var actualBytes = targetStream.ToArray();
        var result = Const.Const.DefaultEncoding.GetString(actualBytes);
        return result;
    }

    private static IEnumerable<TestCaseData> GetTestCases()
    {
        var initialString1 =
            $"1. a{Environment.NewLine}";
        var expectedString1 =
            $"1. a{Environment.NewLine}";
        yield return new TestCaseData(initialString1).Returns(expectedString1)
            .SetName("All lines ended with new line symbol");

        var initialString2 =
            $"1. a{Environment.NewLine}" +
            $"1. a{Environment.NewLine}";
        var expectedString2 =
            $"1. a{Environment.NewLine}" +
            $"1. a{Environment.NewLine}";
        yield return new TestCaseData(initialString2).Returns(expectedString2)
            .SetName("Repeated lines");


        const string initialString3 = "1. a";
        var expectedString3 = $"1. a{Environment.NewLine}";
        yield return new TestCaseData(initialString3).Returns(expectedString3)
            .SetName("Last line is not ended with new line symbol");
    }
}