using System;
using System.Globalization;

namespace naru.formatters
{
    public static class StringFormatters
    {

        /// <summary>
        /// This is a common-sense number formatter which allows you to specify the number of decimal places you want
        /// 
        /// use it by including naru.formatters at the top of your file. Then all your types will get a .ToNumString() method
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="val"></param>
        /// <param name="precision"></param>
        /// <returns></returns>
        public static string ToNumString<T>(this T val, int precision = -1)
        {

            if (precision < 0 || precision > 25 || val is int || val is uint || val is long)
                return (string)Convert.ChangeType(val, typeof(string));
            else
            {
                NumberFormatInfo numFormat = new NumberFormatInfo() {
                    NumberDecimalDigits = precision,
                    NumberGroupSeparator = ","
                };

                if (val is decimal)
                    return string.Format(numFormat, "{0:N}", (T)Convert.ChangeType(val, typeof(decimal)));
                else if (val is double)
                    return string.Format(numFormat, "{0:N}", (T)Convert.ChangeType(val, typeof(double)));
                else if (val is float)
                    return string.Format(numFormat, "{0:N}", (T)Convert.ChangeType(val, typeof(float)));
                else if (val is string)
                    return string.Format(numFormat, "{0:N}", Convert.ToDecimal(val));
                else
                    return (string)Convert.ChangeType(val, typeof(string));
            }

        }
    }
}
