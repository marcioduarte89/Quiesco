namespace Products.API.Features.Properties.CreateRoom
{
    using Core.Enums;
    using MediatR;
    using Models.Output;

    /// <summary>
    /// Create room command
    /// </summary>
    public class CreateRoomCommand : IRequest<Room>
    {
        /// <summary>
        /// Property Id
        /// </summary>
        public int PropertyId { get; set; }

        /// <summary>
        /// Accommodation type for the room
        /// </summary>
        public RoomTypes AccommodationType { get; set; }

        /// <summary>
        /// Nr of occupants for the room
        /// </summary>
        public int NrOfOccupants { get; set; }
    }
}
