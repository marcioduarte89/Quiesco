namespace Availability.API.Features.Get.Profile
{
    using AutoMapper;
    using Models.Input.Room.Update;

    /// <summary>
    /// Input Model
    /// </summary>
    public class Input : Profile {
        /// <summary>
        /// Constructor
        /// </summary>
        public Input() {
            CreateMap<UpdateRoomAvailability, GetRequest>();
        }
    }
}
