namespace Products.API.Features.Properties.CreateProperty.Profile
{
    using AutoMapper;
    using Models.Input.Property.Create;

    /// <summary>
    /// Input Model for creating property Map
    /// </summary>
    public class Input : Profile
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Input()
        {
            CreateMap<CreateProperty, CreatePropertyCommand>();
        }
    }
}
