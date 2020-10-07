namespace Products.API.Models.Input.Room
{
    using Core.Enums;

    /// <summary>
    /// Created a new Room
    /// </summary>
    public class CreateRoom
    {
        /// <summary>
        /// Room Accommodation type
        /// </summary>
        public RoomTypes AccommodationType { get; set; }

        /// <summary>
        /// Nr of Occupants for the room
        /// </summary>
        public int NrOfOccupants { get; set; }
    }
}
