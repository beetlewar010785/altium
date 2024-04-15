namespace Altium.FileSorter.Tests;

public class RowTests
{
    [TestCaseSource(nameof(GetTestCases))]
    public int Should_Compare_Rows(Row left, Row right)
    {
        return left.CompareTo(right);
    }

    private static IEnumerable<TestCaseData> GetTestCases()
    {
        yield return new TestCaseData(new Row(1, "a"), new Row(1, "b"))
            .Returns(-1);

        yield return new TestCaseData(new Row(1, "b"), new Row(1, "a"))
            .Returns(1);

        yield return new TestCaseData(new Row(1, "a"), new Row(2, "a"))
            .Returns(-1);

        yield return new TestCaseData(new Row(2, "a"), new Row(1, "a"))
            .Returns(1);

        yield return new TestCaseData(new Row(1, "a"), new Row(1, "a"))
            .Returns(-1);
    }
}