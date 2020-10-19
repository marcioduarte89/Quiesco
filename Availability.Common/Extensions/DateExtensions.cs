using System;
using System.Collections.Generic;
using System.Text;

namespace Availability.Common.Extensions
{
    using System.Globalization;

    public static class DateExtensions
    {
        public static DateTime Convert(int date)
        {
            return DateTime.ParseExact(date.ToString(), "ddMMyyyy", CultureInfo.InvariantCulture); ;
        }
    }
}
