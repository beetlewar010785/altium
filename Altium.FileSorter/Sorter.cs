using System.Threading.Channels;

namespace Altium.FileSorter;

public class Sorter(IRowSource rowSource, IRowIndex rowIndex, IRowTarget rowTarget)
{
    public async Task Sort()
    {
        const int channelCapacity = 1000000;
        var rowChannel = Channel.CreateBounded<Row>(new BoundedChannelOptions(channelCapacity));

        var publishSourceTask = PublishSource(rowChannel.Writer);
        var writeIndexTask = WriteIndex(rowChannel.Reader);

        await publishSourceTask;
        rowChannel.Writer.Complete();
        await writeIndexTask;

        await WriteToTarget();
    }

    private async Task PublishSource(ChannelWriter<Row> writer)
    {
        var i = 0;
        await foreach (var row in rowSource)
        {
            i++;
            if (i % Const.Const.LogPeriod == 0)
            {
                Console.WriteLine($"Read rows: {i}.");
            }

            await writer.WriteAsync(row);
            await writer.WaitToWriteAsync();
        }

        Console.WriteLine($"{i} rows are read from source.");
    }

    private async Task WriteIndex(ChannelReader<Row> reader)
    {
        var i = 0;
        await foreach (var row in reader.ReadAllAsync())
        {
            i++;
            if (i % Const.Const.LogPeriod == 0)
            {
                Console.WriteLine($"Indexed rows: {i}.");
            }

            rowIndex.Insert(row);
        }

        Console.WriteLine($"{i} rows are indexed.");
    }

    private async Task WriteToTarget()
    {
        var i = 0;
        foreach (var row in rowIndex)
        {
            i++;
            if (i % Const.Const.LogPeriod == 0)
            {
                Console.WriteLine($"Written rows: {i}.");
            }

            await rowTarget.Write(row);
        }

        Console.Write($"{i} rows are written to target.");
    }
}