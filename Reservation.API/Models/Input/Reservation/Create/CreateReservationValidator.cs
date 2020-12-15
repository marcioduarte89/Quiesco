namespace Reservation.API.Models.Input.Reservation.Create
{
    using FluentValidation;

    /// <summary>
    /// Create Reservation validator
    /// </summary>
    public class CreateReservationValidator : AbstractValidator<CreateReservation>
    {
        /// <summary>
        /// Validator constructor
        /// </summary>
        public CreateReservationValidator()
        {
            RuleFor(a => a.PropertyId)
                .GreaterThan(0)
                .WithMessage(x => "Property Id must be greater than 0");

            RuleFor(a => a.RoomId)
                .GreaterThan(0)
                .WithMessage(x => "Room Id must be greater than 0");

            RuleFor(a => a.CheckIn)
               .LessThan(a => a.CheckOut)
               .WithMessage(x => "CheckOut must be greater than CheckIn.");

            RuleFor(x => x.NumberOfOccupants)
                .GreaterThan(0)
                .WithMessage(x => "Number of occupants must be greater than 0");

            RuleFor(x => x.User)
                .NotNull()
                .WithMessage(x => "User details must be set");
        }
    }
}
