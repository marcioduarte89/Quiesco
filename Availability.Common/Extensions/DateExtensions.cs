namespace Availability.Common.Extensions
{
    using System;
    using System.Collections.Generic;

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

        public static IEnumerable<DateTime> ToSlotList(this DateTime @in, DateTime @out) {

            var slot = new List<DateTime>();

            // unfold date ranges onto days within that range
            for (var date = @in; date <= @out; date = date.Date.AddDays(1))
            {
                slot.Add(date.Date);
            }


            return slot;
        }
    }
}
