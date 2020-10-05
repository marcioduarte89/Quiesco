namespace Products.Core.Models
{
    using Enums;
    using System.Collections.Generic;
    using Common.Exceptions;

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
    }
}
