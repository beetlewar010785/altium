namespace Altium.RandomRowsGenerator;

public class LineGenerator(
    int intMinValue,
    int intMaxValue,
    int minStringLength,
    int maxStringLength)
{
    private readonly Random _random = new();

    public int GenerateInt()
    {
        return _random.Next(intMinValue, intMaxValue);
    }

    public string GenerateString()
    {
        var length = _random.Next(minStringLength, maxStringLength);
        return new string(Enumerable
            .Range(0, length)
            .Select(_ => (char)_random.Next('A', 'z'))
            .ToArray());
    }
}