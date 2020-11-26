namespace Products.Core.Models
{
    using Enums;
    using System.Collections.Generic;
    using System.Linq;
    using Common.Exceptions;

    /// <summary>
    /// Property domain entity
    /// </summary>
    public class Property : BaseEntity
    {
        /// <summary>
        /// Property rooms
        /// </summary>
        private readonly ICollection<Room> _rooms;

        /// <summary>
        /// Property Id
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Property type
        /// </summary>
        public PropertyTypes Type { get; private set; }

        /// <summary>
        /// Property Name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Rooms - external entry point
        /// </summary>
        public IEnumerable<Room> Rooms => _rooms;

        /// <summary>
        /// EF constructor
        /// </summary>
        private Property() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="type">property type</param>
        /// <param name="name">property name</param>
        /// <exception cref="ProductException">If parameters are invalid</exception>
        public Property(PropertyTypes type, string name)
        {
            _rooms = new List<Room>();
            SetName(name);
            Type = type;
        }

        /// <summary>
        /// Sets property name
        /// </summary>
        /// <param name="name">property name</param>
        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ProductException(ProductException.INVALID_DATA, "Name cannot be null or empty");
            }

            Name = name;
        }

        /// <summary>
        /// Adds a new room to the property
        /// </summary>
        /// <param name="room">Room details</param>
        public void AddRoom(Room room)
        {
            if (room == null)
            {
                throw new ProductException(ProductException.INVALID_DATA, "room cannot be null");
            }

            _rooms.Add(room);
        }

        /// <summary>
        /// Gets an instance of the <see cref="Room"/>
        /// </summary>
        /// <param name="roomId">Room id</param>
        /// <returns>Returns an instance of <see cref="Room"/></returns>
        /// <exception cref="ProductException">If rooms is not included</exception>
        /// <exception cref="ProductException">If room is not found</exception>
        public Room GetRoom(int roomId)
        {
            var room = _rooms.FirstOrDefault(x => x.Id == roomId);

            if (room == null)
            {
                throw new ProductException(ProductException.NOT_FOUND, $"Room with id {roomId} cannot be found");
            }

            return room;
        }
    }
}
