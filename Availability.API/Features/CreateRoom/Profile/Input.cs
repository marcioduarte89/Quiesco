namespace Availability.API.Features.CreateRoom.Profile
{
    using AutoMapper;
    using Models.Input.Room.Create;

    /// <summary>
    /// Input Model
    /// </summary>
    public class Input : Profile {
       
        /// <summary>
        /// Constructor
        /// </summary>
        public Input() {
            CreateMap<CreateRoom, CreateCommand>();
        }
    }
}
