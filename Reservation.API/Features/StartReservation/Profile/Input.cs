namespace Availability.API.Features.StartReservation.Profile
{
    using AutoMapper;
    using Reservations.API.Models.Input.Common;
    using SharedKernel.Messages.Commands.Reservation;
    using InputModel = Reservation.API.Models.Input.Reservation.Create;

    /// <summary>
    /// Input Model
    /// </summary>
    public class Input : Profile {
       
        /// <summary>
        /// Constructor
        /// </summary>
        public Input() {
            CreateMap<User, SharedKernel.Messages.Common.User>();
            CreateMap<InputModel.CreateReservation, StartReservation>();
        }
    }
}
