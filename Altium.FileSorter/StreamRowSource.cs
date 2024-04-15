using System.Text;

namespace Altium.FileSorter;

public class StreamRowSource(TextReader textReader, IRowParser rowParser) : IRowSource
{
    public IAsyncEnumerator<Row> GetAsyncEnumerator(CancellationToken cancellationToken = new())
    {
        return new RowAsyncEnumerator(textReader, rowParser);
    }

    private class RowAsyncEnumerator(
        TextReader streamReader,
        IRowParser rowParser
    )
        : IAsyncEnumerator<Row>
    {
        public Row Current { get; private set; } = null!;

        public async ValueTask<bool> MoveNextAsync()
        {
            var line = await streamReader.ReadLineAsync();
            if (line == null)
            {
                return false;
            }

            Current = rowParser.ToRow(line);
            return true;
        }

        public ValueTask DisposeAsync()
        {
            return ValueTask.CompletedTask;
        }
    }
}