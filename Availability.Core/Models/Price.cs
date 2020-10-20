namespace Availability.Core.Models
{
    using Availability.Common.Exceptions;
    using System;
    using Common.Extensions;

    public class Price
    {
        /// <summary>
        /// Date when the price is to be set
        /// </summary>
        public int Date { get; private set; }

        /// <summary>
        /// Value for the price between FromDate to ToDate
        /// </summary>
        public decimal Value { get; private set; }

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="date">Date where the price will be applied</param>
        /// <param name="value">Price value</param>
        public Price(int date, decimal value)
        {
            ValidateDate(date);
            ValidatePrice(value);
            Date = date;
            Value = value;
        }

        /// <summary>
        /// Sets price value
        /// </summary>
        /// <param name="value">Price value</param>
        /// <exception cref="AvailabilityException">Throws <see cref="AvailabilityException"/>If value is invalid</exception>
        public void SetPriceValue(decimal value)
        {
            ValidatePrice(value);
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
        /// Validates date
        /// </summary>
        /// <param name="date">date</param>
        /// <exception cref="AvailabilityException">Throws <see cref="AvailabilityException"/>Date ranges are invalid</exception>
        private void ValidateDate(int date)
        {
            if (DateExtensions.Convert(date) < DateTime.Now)
            {
                throw new AvailabilityException(AvailabilityException.INVALID_DATA, "Date cannot be less than today's date");
            }
        }

        public override int GetHashCode()
        {
             return Date.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is Price price)
            {
                return Date == price.Date;
            }

            return  false;
        }
    }
}
