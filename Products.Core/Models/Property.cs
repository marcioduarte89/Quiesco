namespace Products.Core.Models
{
    using Enums;
    using System.Collections.Generic;

    public class Property
    {
        public int Id { get; private set; }

        public PropertyTypes Type { get; private set; }

        public string Name { get; private set; }

        private ICollection<Room> _rooms;

        public IEnumerable<Room> Rooms => _rooms;

        private Property() { }

        public Property(PropertyTypes type, string name)
        {
            Name = name;
            _rooms = new List<Room>();
            Type = type;
        }
    }
}
