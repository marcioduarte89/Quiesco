using System;

namespace Availability.API.Models.Output
{
    public class Price
    {
        /// <summary>
        /// Date when the price is to be set
        /// </summary>
        public int Date { get; set; }

        /// <summary>
        /// Value for the price between FromDate to ToDate
        /// </summary>
        public decimal Value { get; set; }
    }
}
