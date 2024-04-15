using System.Text;

namespace Altium.Const;

public abstract class Const
{
    public const int LogPeriod = 100000;
    public static Encoding DefaultEncoding => Encoding.ASCII;
}