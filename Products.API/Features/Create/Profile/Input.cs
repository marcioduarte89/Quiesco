namespace Products.API.Features.Create.Profile
{
    using AutoMapper;
    using Models.Input;
    
    public class Input : Profile
    {
        public Input()
        {
            CreateMap<Property, CreateCommand>();
        }
    }
}
