namespace Products.API.Features.Properties.Create.Profile
{
    using AutoMapper;
    using Models.Input.Property;

    public class Input : Profile
    {
        public Input()
        {
            CreateMap<CreateProperty, CreateCommand>();
        }
    }
}
