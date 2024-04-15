namespace Altium.FileSorter;

public class Row(int left, string right) : IComparable<Row>
{
    public int Left { get; } = left;
    public string Right { get; } = right;

    public int CompareTo(Row? other)
    {
        if (other == null)
        {
            return 1;
        }

        var result = string.Compare(Right, other.Right, StringComparison.Ordinal);
        result = result != 0 ? result : Left.CompareTo(other.Left);
        return
            result == 0
                ? -1
                : result; // hack - we are using sorting lib that does not allow to add insert equal elements, so we randomly select row
    }

    private bool Equals(Row other)
    {
        return Left == other.Left && Right == other.Right;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((Row)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Left, Right);
    }

    public override string ToString()
    {
        return $"{Left}. {Right}";
    }
}