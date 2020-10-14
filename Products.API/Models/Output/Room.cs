namespace Products.API.Models.Output
{
    using Core.Enums;

    /// <summary>
    /// Output model for the Room
    /// </summary>
    public class Room
    {
        /// <summary>
        /// Room Id
        /// </summary>
        public int RoomId { get; set; }

        /// <summary>
        /// Room Accommodation Type
        /// </summary>
        public RoomTypes AccommodationType { get; set; }

        /// <summary>
        /// Nr of occupants for the Room
        /// </summary>
        public int NrOfOccupants { get; set; }
    }
}
