namespace SharedKernel.Messages.Events.Reservation
{
    using System;

    /// <summary>
    /// Property existance verified event
    /// </summary>
    public class PropertyExistanceVerified
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

        /// <summary>
        /// Whether a property exists or not
        /// </summary>
        public bool Exists { get; set; }
    }
}
