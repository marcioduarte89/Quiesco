namespace Availability.API.Models.Input.Room.Common
{
    using System;
    using Availability.Common.Exceptions;

    /// <summary>
    /// Room price
    /// </summary>
    public class Price
    {
        /// <summary>
        /// From Date when the price is to be set
        /// ddmmyyyy - format used to simplify queries
        /// </summary>
        public int FromDate { get; set; }

        /// <summary>
        /// To Date until when the price is to be set
        /// ddmmyyyy - format used to simplify queries
        /// </summary>
        public int ToDate { get; set; }

        /// <summary>
        /// Value for the price between FromDate to ToDate
        /// </summary>
        public decimal Value { get; set; }
    }
}
