namespace Availability.Core.Models
{
    using Availability.Common.Exceptions;
    using System;

    public class Price
    {
        /// <summary>
        /// From Date when the price is to be set
        /// </summary>
        public int FromDate { get; private set; }

        /// <summary>
        /// To Date until when the price is to be set
        /// </summary>
        public int ToDate { get; private set; }

        /// <summary>
        /// Value for the price between FromDate to ToDate
        /// </summary>
        public decimal Value { get; private set; }

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="fromDate">From date where the price will be applied</param>
        /// <param name="toDate">From date where the price will be applied</param>
        /// <param name="value">Price value</param>
        public Price(int fromDate, int toDate, decimal value)
        {
            ValidateDateRanges(fromDate, toDate);
            ValidatePrice(value);
            FromDate = fromDate;
            ToDate = toDate;
            Value = value;
        }

        /// <summary>
        /// Validates price value
        /// </summary>
        /// <param name="value">price value</param>
        /// <exception cref="AvailabilityException">Throws <see cref="AvailabilityException"/>If value is invalid</exception>
        private void ValidatePrice(decimal value)
        {
            if (value < 0)
            {
                throw new AvailabilityException(AvailabilityException.INVALID_DATA, $"Value {value} is not valid. Needs to be > than 0");
            }
        }

        /// <summary>
        /// Validates date ranges
        /// </summary>
        /// <param name="fromDate">From date</param>
        /// <param name="toDate"></param>
        /// <exception cref="AvailabilityException">Throws <see cref="AvailabilityException"/>Date ranges are invalid</exception>
        private void ValidateDateRanges(int fromDate, int toDate)
        {
            if (fromDate >= toDate)
            {
                throw new AvailabilityException(AvailabilityException.INVALID_DATA, "Date from needs to be less than toDate");
            }
        }
    }
}
