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

        public static int ToInt(this DateTime dateTime)
        {
            return int.Parse(dateTime.ToString("dMMyyyy"));
        }

        public static bool IsValid(int date)
        {
            if (DateTime.TryParseExact(date.ToString(), "ddMMyyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            {
                return true;
            }

            return false;
        }

        public static bool IsValid(DateTime date)
        {
            if(date.CompareTo(DateTime.Now) >= 0)
            {
                return true;
            }

            return false;
        }
    }
}
