namespace Products.API.Models.Input.Room
{
    using Core.Enums;

    public class UpdateRoom
    {
        public RoomTypes AccommodationType { get; set; }
        public int NrOfOccupants { get; set; }
    }
}
