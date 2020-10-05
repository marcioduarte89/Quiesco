namespace Products.API.Features.Properties.Update.Profile
{
    using AutoMapper;
    using Models.Input.Property;
    using Update;

    public class Input : Profile
    {
        public Input()
        {
            CreateMap<UpdateRoom, UpdateCommand>();
        }
    }
}
