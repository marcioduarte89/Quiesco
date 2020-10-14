namespace Products.API.Features.Properties.CreateRoom.Profile
{
    using AutoMapper;
    using Core.Models;

    /// <summary>
    /// Create room output model Map
    /// </summary>
    public class Output : Profile
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Output()
        {
            CreateMap<Room, Models.Output.Room>();
        }
    }
}
