using System;

namespace Availability.API.Models.Output
{
    public class Price
    {
        /// <summary>
        /// From Date when the price is to be set
        /// </summary>
        public DateTime FromDate { get; set; }

        /// <summary>
        /// To Date until when the price is to be set
        /// </summary>
        public DateTime ToDate { get; set; }

        /// <summary>
        /// Value for the price between FromDate to ToDate
        /// </summary>
        public decimal Value { get; set; }
    }
}
