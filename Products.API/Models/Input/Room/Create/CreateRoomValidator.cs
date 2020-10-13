using FluentValidation;

namespace Products.API.Models.Input.Room.Create
{
    /// <summary>
    /// Create room validator
    /// </summary>
    public class CreateRoomValidator : AbstractValidator<CreateRoom>
    {
        /// <summary>
        /// Validator constructor
        /// </summary>
        public CreateRoomValidator()
        {
            RuleFor(a => a.NrOfOccupants)
                .GreaterThan(0)
                .WithMessage("Number of occupants need to be greater than 0");

            RuleFor(a => a.AccommodationType)
                .IsInEnum()
                .WithMessage("Accommodation type must have a valid type");
        }
    }
}
