namespace Products.API.Features.Rooms.Create.Profile
{
    using AutoMapper;
    using Models.Input.Room;

    public class Input : Profile
    {
        public Input()
        {
            CreateMap<CreateRoom, CreateCommand>();
        }
    }
}
