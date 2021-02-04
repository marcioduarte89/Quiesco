namespace SharedKernel.Messages.Commands.Reservation
{
    using System;

    /// <summary>
    /// Command used to verify if property exists
    /// </summary>
    public class VerifyPropertyExists
    {
        /// <summary>
        /// Property Id
        /// </summary>
        public Guid ReservationId { get; set; }

        /// <summary>
        /// Property Id
        /// </summary>
        public int PropertyId { get; set; }

        /// <summary>
        /// Room Id
        /// </summary>
        public int RoomId { get; set; }
    }
}
