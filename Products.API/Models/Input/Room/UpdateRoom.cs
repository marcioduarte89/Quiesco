namespace Products.API.Models.Input.Room
{
    using Core.Enums;

    /// <summary>
    /// Updates the Room
    /// </summary>
    public class UpdateRoom
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
