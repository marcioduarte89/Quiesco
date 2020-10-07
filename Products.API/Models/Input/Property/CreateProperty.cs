namespace Products.API.Models.Input.Property
{
    using Core.Enums;

    /// <summary>
    /// Created a new Property
    /// </summary>
    public class CreateProperty
    {
        /// <summary>
        /// Property type
        /// </summary>
        public PropertyTypes Type { get; set; }

        /// <summary>
        /// Property Name
        /// </summary>
        public string Name { get; set; }
    }
}
