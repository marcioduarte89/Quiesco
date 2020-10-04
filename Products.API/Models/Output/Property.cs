namespace Products.API.Models.Output
{
    using Core.Enums;

    public class Property
    {
        public PropertyTypes Type { get; private set; }

        public string Name { get; private set; }
    }
}
