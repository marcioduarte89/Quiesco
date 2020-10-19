namespace Availability.API.Features.Delete.Profile
{
    using AutoMapper;
    using Models.Input.Room.Update;

    /// <summary>
    /// Input Model for deleting
    /// </summary>
    public class Input : Profile {
        /// <summary>
        /// Constructor
        /// </summary>
        public Input() {
            CreateMap<UpdateRoomAvailability, DeleteCommand>();
        }
    }
}
