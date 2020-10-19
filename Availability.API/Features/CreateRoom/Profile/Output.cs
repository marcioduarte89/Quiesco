namespace Availability.API.Features.CreateRoom.Profile {
    using AutoMapper;
    using Core.Models;

    /// <summary>
    /// Output model Map
    /// </summary>
    public class Output : Profile {
        /// <summary>
        /// Constructor
        /// </summary>
        public Output() {
            CreateMap<Room, Models.Output.Room>();
        }
    }
}
