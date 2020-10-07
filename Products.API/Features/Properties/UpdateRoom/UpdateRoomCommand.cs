namespace Products.API.Features.Properties.UpdateRoom
{
    using Core.Enums;
    using MediatR;
    using Models.Output;

    /// <summary>
    /// Update property command
    /// </summary>
    public class UpdateRoomCommand : IRequest<Room>
    {
        /// <summary>
        /// Property Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Room Id
        /// </summary>
        public int RoomId { get; set; }

        /// <summary>
        /// Accommodation Type
        /// </summary>
        public RoomTypes AccommodationType { get; set; }

        /// <summary>
        /// Number of Occupants
        /// </summary>
        public int NrOfOccupants { get; set; }
    }
}
