namespace Products.API.Models.Input.Property
{
    using Core.Enums;

    public class CreateProperty
    {
        public PropertyTypes Type { get; set; }
        public string Name { get; set; }
    }
}
