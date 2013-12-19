using System;
using System.Linq;
using System.Text;

namespace WellEmulatorMvc.Extensions
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

        public static int ToPerTime(this TimeSpan timeSpan, ref string units)
        {
            var value = timeSpan.TotalMilliseconds;
            var factor = 1000;
            if ((int) (value / factor) <= 1)
            {
                units = "Сек";
                return (int) (factor / value);
            }
            factor *= 60;
            if ((int) (value / factor) <= 1)
            {
                units = "Мин";
                return (int) (factor / value);
            }
            factor *= 60;
            if ((int) (value / factor) <= 1)
            {
                units = "Час";
                return (int) (factor / value);
            }
            factor *= 24;
            if ((int) (value / factor) <= 1)
            {
                units = "Сут";
                return (int) (factor / value);
            }
            return 0;
        }

        public static int ToPeriod(this TimeSpan timeSpan, ref string units)
        {
            if (timeSpan.Seconds != 0)
            {
                units = "Сек";
                return timeSpan.Seconds;
            }
            if (timeSpan.Minutes != 0)
            {
                units = "Мин";
                return timeSpan.Minutes;
            }
            if (timeSpan.Hours != 0)
            {
                units = "Час";
                return timeSpan.Hours;
            }
            if (timeSpan.Days != 0)
            {
                units = "Сут";
                return timeSpan.Days;
            }
            return 0;
        }
    }
}
