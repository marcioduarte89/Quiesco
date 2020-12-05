using System;

namespace SharedKernel.Messages.Commands
{
    public class Example {
        public int Id { get; set; }

        /// <summary>
        /// Internal Reservation Id
        /// </summary>
        public Guid ReservationId { get; set; }
    }
}
