namespace Availability.API.Models.Input.Room.Common
{
    using System;

    /// <summary>
    /// Room price
    /// </summary>
    public class Price
    {
        /// <summary>
        /// Date when the price is to be set
        /// ddmmyyyy - format used to simplify queries
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Value for the price between FromDate to ToDate
        /// </summary>
        public decimal Value { get; set; }
    }
}
