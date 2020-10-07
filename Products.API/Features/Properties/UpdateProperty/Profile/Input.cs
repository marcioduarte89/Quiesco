namespace Products.API.Features.Properties.UpdateProperty.Profile
{
    using AutoMapper;
    using Models.Input.Property;

    /// <summary>
    /// Input Model for updating property Map
    /// </summary>
    public class Input : Profile
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Input()
        {
            CreateMap<UpdateProperty, UpdatePropertyCommand>();
        }
    }
}
