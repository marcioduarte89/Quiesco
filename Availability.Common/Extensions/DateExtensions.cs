namespace Availability.Common.Extensions
{
    using System;

    public static class DateExtensions
    {
        public static bool IsValid(DateTime date)
        {
            if(date.CompareTo(DateTime.Now) >= 0)
            {
                return true;
            }

            return false;
        }

        public static string ConvertToISODate(this DateTime date)
        {
            return date.ToString("yyyy-MM-ddTHH:mm:ssZ");
        }

        public static string GetMongoISODate(this DateTime date)
        {
            return $"new ISODate(\"{date.ConvertToISODate()}\")";
        }
    }
}
