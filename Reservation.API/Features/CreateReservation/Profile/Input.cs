namespace Availability.API.Features.CreateRoom.Profile
{
    using AutoMapper;
    using Reservation.API.Models.Input.Reservation.Create;
    using Reservations.API.Models.Input.Common;
    using SharedKernel.Messages.Commands;

    /// <summary>
    /// Input Model
    /// </summary>
    public class Input : Profile {
       
        /// <summary>
        /// Constructor
        /// </summary>
        public Input() {
            CreateMap<User, SharedKernel.Messages.Common.User>();
            CreateMap<CreateReservation, StartReservation>();
        }
    }
}
