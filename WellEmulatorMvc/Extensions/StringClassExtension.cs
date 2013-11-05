using System;
using System.Linq;
using System.Text;

namespace WellEmulatorMvc.Extensions
{
    public static class StringClassExtension
    {
        public static string RemoveTabs(this string @string)
        {
            return
                @string.Where(@char => !"\n\t\r".Contains(@char))
                    .Aggregate(new StringBuilder(), (builder, @char) => builder.Append(@char))
                    .ToString();
        }

        //// Extension Method 1: IfNotNull
        //public static U IfNotNull<T, U>(this T t, Func<T, U> function)
        //{
        //    return t != null ? function(t) : default(U);
        //}

        //// Extension Method 2: UppercaseFirstLetter
        //public static string UppercaseFirstLetter(this string value)
        //{
        //    if (value.Length > 0)
        //    {
        //        char[] array = value.ToCharArray();
        //        array[0] = char.ToUpper(array[0]);
        //        return new string(array);
        //    }
        //    return value;
        //}
    }
}
