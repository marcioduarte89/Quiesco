namespace Reservations.API.Models
{
    using NServiceBus;
    using SharedKernel.Messages.Common;
    using System;

    /// <summary>
    /// Reservation data relative to the Saga
    /// </summary>
    public class ReservationSagaData : IContainSagaData
    {
        /// <summary>
        ///  Gets/sets the Id of the process. Do NOT generate this value in saga code. The
        ///  value of the Id will be generated automatically to provide the best performance
        ///  for saving in a database.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Contains the return address of the endpoint that caused the process to run.
        /// </summary>
        public string Originator { get; set; }

        /// <summary>Contains the Id of the message which caused the saga to start. 
        /// This is needed so that when we reply to the Originator,
        /// any registered callbacks will be fired correctly.
        /// </summary>
        public string OriginalMessageId { get; set; }

        /// <summary>
        /// Internal Reservation Id
        /// </summary>
        public Guid ReservationId { get; set; }

        /// <summary>
        /// Holds if the reservation for the room has availability
        /// </summary>
        public bool HasAvailability { get; set; }

        /// <summary>
        /// Has payment been processed
        /// </summary>
        public bool HasPaymentBeingProcessed { get; set; }

        /// <summary>
        /// Property Id
        /// </summary>
        public int PropertyId { get; set; }

        /// <summary>
        /// Room Id
        /// </summary>
        public int RoomId { get; set; }

        /// <summary>
        /// Reservation Check-in
        /// </summary>
        public DateTime CheckIn { get; set; }

        /// <summary>
        /// Reservation Check-out
        /// </summary>
        public DateTime CheckOut { get; set; }

        /// <summary>
        /// Number of occupants
        /// </summary>
        public int NumberOfOccupants { get; set; }

        /// <summary>
        /// Reservation details for the user
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Reservation Status
        /// </summary>
        public Status ReservationStatus { get; set; }

        /// <summary>
        /// Whether the notification has been sent to the property
        /// </summary>
        public bool PropertyNotificationSent { get; set; }

        /// <summary>
        /// Wheter the notification has been sent to the user
        /// </summary>
        public bool UserNotificationSent { get; set; }
    }
}
