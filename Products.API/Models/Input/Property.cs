namespace Products.API.Models.Input
{
    using Core.Enums;

    public class Property
    {
        public PropertyTypes Type { get; set; }

        public string Name { get; set; }
    }
}
