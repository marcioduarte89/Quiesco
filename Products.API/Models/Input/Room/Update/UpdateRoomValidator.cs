using FluentValidation;

namespace Products.API.Models.Input.Room.Update
{
    /// <summary>
    /// Update room validator
    /// </summary>
    public class UpdateRoomValidator : AbstractValidator<UpdateRoom>
    {
        /// <summary>
        /// Validator constructor
        /// </summary>
        public UpdateRoomValidator()
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
