namespace Products.Core.Models
{

    using Enums;
    public class Room
    {
        public int Id { get; private set; }

        public RoomTypes AccommodationType { get; private set; }

        public int NrOfOccupants { get; private set; }

        public Property Property { get; private set; }

        private Room() { }

        public Room(RoomTypes accommodationType, int nrOfOccupants)
        {
            AccommodationType = accommodationType;
            NrOfOccupants = nrOfOccupants;
        }
    }
}
