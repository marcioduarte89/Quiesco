namespace Products.API.Features.Rooms.Create.Profile
{
    using AutoMapper;
    using Core.Models;

    public class Output : Profile
    {
        public Output()
        {
            CreateMap<Room, Models.Output.Room>();
        }
    }
}
