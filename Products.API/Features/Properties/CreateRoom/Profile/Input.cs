namespace Products.API.Features.Properties.CreateRoom.Profile
{
    using AutoMapper;
    using Models.Input.Room;

    /// <summary>
    /// Input Model for creating room Map
    /// </summary>
    public class Input : Profile
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Input()
        {
            CreateMap<CreateRoom, CreateRoomCommand>();
        }
    }
}
