namespace Reservations.API.Models
{
    using NServiceBus;
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
    }
}
