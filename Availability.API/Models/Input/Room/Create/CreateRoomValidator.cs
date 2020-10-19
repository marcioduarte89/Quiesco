namespace Availability.API.Models.Input.Room.Create
{
    using FluentValidation;

    /// <summary>
    /// Create AddRoomAvailability validator
    /// </summary>
    public class CreateRoomValidator : AbstractValidator<CreateRoom>
    {
        /// <summary>
        /// Validator constructor
        /// </summary>
        public CreateRoomValidator()
        {
            RuleFor(x => x.RoomId)
                .GreaterThan(0)
                .WithMessage(x => "Room Id must be greater than 0");

            RuleFor(x => x.DefaultPrice)
                .GreaterThan(0)
                .WithMessage(x => "Default price must be greater than 0");
        }
    }
}
