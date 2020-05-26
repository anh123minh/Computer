using System;
using System.Globalization;

namespace Computer.Common
{
    public class MethodHelper
    {
        public static DateTime StringToDayMonthYear(string date)
        {
            return DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.GetCultureInfo("vi-VN"));
        }

        public static DateTime StringToMonthDayYear(string date)
        {
            return DateTime.ParseExact(date, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
        }
    }
}
