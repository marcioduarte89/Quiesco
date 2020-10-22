namespace Availability.Common.Extensions
{
    using System;
    using System.Globalization;

    public static class DateExtensions
    {
        public static DateTime Convert(int date)
        {
            return DateTime.ParseExact(date.ToString(), "ddMMyyyy", CultureInfo.InvariantCulture); ;
        }

        public static bool IsValid(int date)
        {
            if (DateTime.TryParseExact(date.ToString(), "ddMMyyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            {
                return true;
            }

            return false;
        }
    }
}
