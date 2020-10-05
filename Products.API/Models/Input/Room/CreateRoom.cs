namespace Products.API.Models.Input.Room
{
    using Core.Enums;

    public class CreateRoom
    {
        public RoomTypes AccommodationType { get; set; }
        public int NrOfOccupants { get; set; }
    }
}
