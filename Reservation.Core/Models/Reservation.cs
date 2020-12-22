namespace Reservations.Core.Models
{
    using global::Reservation.Common.Exceptions;
    using System;

    /// <summary>
    /// Reservation domain model
    /// </summary>
    public class Reservation
    {
        /// <summary>
        /// Property Id
        /// </summary>
        public Guid ReservationId { get; private set; }

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
        /// User in reservation
        /// </summary>
        public User User { get; private set; }

        /// <summary>
        /// Reservation status
        /// </summary>
        public Status Status { get; private set; } 

        /// <summary>
        /// Holds reason if cancellation has been requested for the reservation
        /// </summary>
        public string CancellationReason { get; private set; }

        /// <summary>
        /// Creates the reservation
        /// </summary>
        /// <param name="reservationProperties">reservation properties</param>
        /// <param name="roomId">user details</param>
        /// <returns>Returns an instance of <see cref="Reservation"/></returns>
        public static Reservation Create(ReservationProperties reservationProperties, User user)
        {
            Validate(reservationProperties, user);

            return new Reservation()
            {
                PropertyId = reservationProperties.PropertyId,
                RoomId = reservationProperties.RoomId,
                CheckIn = reservationProperties.CheckIn,
                CheckOut = reservationProperties.CheckOut,
                NumberOfOccupants = reservationProperties.NumberOfOccupants,
                User = user,
                Status = Status.Created
            };
        }

        /// <summary>
        /// Validates the reservation details
        /// </summary>
        /// <param name="propertyId"></param>
        /// <param name="roomId"></param>
        /// <param name="checkIn"></param>
        /// <param name="checkOut"></param>
        /// <param name="nrOfOccupants"></param>
        private static void Validate(ReservationProperties reservationProperties, User user)
        {
            void ThrowIfNull<T>(T property)
            {
                if(property == null)
                {
                    throw new ReservationException(ReservationException.INVALID_DATA, $"{property} cannot be null");
                }
            }

            ThrowIfNull(reservationProperties);
            ThrowIfNull(user);
        }

        /// <summary>
        /// Completes the reservations. Sets its status to processed
        /// </summary>
        public void CompleteReservation()
        {
            Status = Status.Processed;
        }

        /// <summary>
        /// Cancels the reservations. Sets its status to cancelled
        /// </summary>
        public void CancelReservation(string reason)
        {
            if (string.IsNullOrEmpty(reason))
            {
                throw new ReservationException(ReservationException.INVALID_DATA, $"{nameof(reason)} cannot be null or empty");
            }

            CancellationReason = reason;
            Status = Status.Cancelled;
        }
    }
}
