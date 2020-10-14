namespace Products.API.Features.Properties.UpdateRoom.Profile
{
    using AutoMapper;
    using Models.Input.Room.Update;

    /// <summary>
    /// Input Model for updating room Map
    /// </summary>
    public class Input : Profile
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Input()
        {
            CreateMap<UpdateRoom, UpdateRoomCommand>();
        }
    }
}
