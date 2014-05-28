using System.Linq;
using System.Text;

namespace WellEmulatorService.Extension
{
    public static class Extension
    {
        public static string RemoveTabs(this string @string)
        {
            return
                @string.Where(@char => !"\n\t\r".Contains(@char))
                    .Aggregate(new StringBuilder(), (builder, @char) => builder.Append(@char))
                    .ToString();
        }
    }
}
