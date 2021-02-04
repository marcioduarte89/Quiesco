namespace Reservations.Core.Models
{
    using global::Reservation.Common.Exceptions;
    using System;

    /// <summary>
    /// Reservation properties domain model
    /// </summary>
    public class ReservationProperties
    {
        /// <summary>
        /// Property Id
        /// </summary>
        public int PropertyId { get; private set; }

        /// <summary>
        /// Room Id
        /// </summary>
        public int RoomId { get; private set; }

        /// <summary>
        /// Checkin date
        /// </summary>
        public DateTime CheckIn { get; private set; }

        /// <summary>
        /// Checkout date
        /// </summary>
        public DateTime CheckOut { get; private set; }

        /// <summary>
        /// Number of occupants for the reservation
        /// </summary>
        public int NumberOfOccupants { get; private set; }

        /// <summary>
        /// Creates the reservation properties
        /// </summary>
        /// <param name="propertyId"></param>
        /// <param name="roomId"></param>
        /// <param name="checkIn"></param>
        /// <param name="checkOut"></param>
        /// <param name="nrOfOccupants"></param>
        /// <returns></returns>
        public static ReservationProperties Create(int propertyId, int roomId, DateTime checkIn, DateTime checkOut, int nrOfOccupants)
        {
            Validate(propertyId, roomId, checkIn, checkOut, nrOfOccupants);

            return new ReservationProperties()
            {
                PropertyId = propertyId,
                RoomId = roomId,
                CheckIn = checkIn,
                CheckOut = checkOut,
                NumberOfOccupants = nrOfOccupants
            };
        }

        /// <summary>
        /// Validates the reservation properties
        /// </summary>
        /// <param name="propertyId"></param>
        /// <param name="roomId"></param>
        /// <param name="checkIn"></param>
        /// <param name="checkOut"></param>
        /// <param name="nrOfOccupants"></param>
        private static void Validate(int propertyId, int roomId, DateTime checkIn, DateTime checkOut, int nrOfOccupants)
        {
            if (propertyId <= 0)
            {
                throw new ReservationException(ReservationException.INVALID_DATA, "Property id can't be <= 0");
            }

            if (roomId <= 0)
            {
                throw new ReservationException(ReservationException.INVALID_DATA, "Room id can't be <= 0");
            }

            if (nrOfOccupants <= 0)
            {
                throw new ReservationException(ReservationException.INVALID_DATA, "Room id can't be <= 0");
            }

            if(checkIn.CompareTo(checkOut) > 0)
            {
                throw new ReservationException(ReservationException.INVALID_DATA, "CheckIn date cannot be later than CheckOut date");
            }
        }
    }
}
