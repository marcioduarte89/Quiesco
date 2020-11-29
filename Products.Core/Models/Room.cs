namespace Products.Core.Models
{
    using Common.Exceptions;
    using Enums;

    /// <summary>
    /// Room domain entity
    /// </summary>
    public class Room : BaseEntity
    {
        /// <summary>
        /// Room id
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Room Accommodation type
        /// </summary>
        public RoomTypes AccommodationType { get; private set; }

        /// <summary>
        /// Nr of occupants to the room
        /// </summary>
        public int NrOfOccupants { get; private set; }

        /// <summary>
        /// Property reference from the room
        /// </summary>
        public Property Property { get; private set; }

        /// <summary>
        /// EF constructor
        /// </summary>
        private Room() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="accommodationType">Accommodation type</param>
        /// <param name="nrOfOccupants">Number of Occupants</param>
        /// <exception cref="ProductException">If parameters are invalid</exception>
        public Room(RoomTypes accommodationType, int nrOfOccupants)
        {
            AccommodationType = accommodationType;
            ValidateInput(nrOfOccupants);
            NrOfOccupants = nrOfOccupants;
        }

        /// <summary>
        /// Validates Input
        /// Might want to be mor specific here in the future
        /// </summary>
        /// <param name="nrOfOccupants"></param>
        private void ValidateInput(int nrOfOccupants)
        {
            if (nrOfOccupants <= 0)
            {
                throw new ProductException(ProductException.INVALID_DATA, $"nrOfOccupants value {nrOfOccupants} is invalid - can't be <= than 0,");
            }
        }

        /// <summary>
        /// Updates the room
        /// </summary>
        /// <param name="accommodationType">Accommodation type</param>
        /// <param name="nrOfOccupants">Nr of Occupants</param>
        public void UpdateRoom(RoomTypes accommodationType, int nrOfOccupants)
        {
            AccommodationType = accommodationType;
            ValidateInput(nrOfOccupants);
            NrOfOccupants = nrOfOccupants;
        }
    }
}
