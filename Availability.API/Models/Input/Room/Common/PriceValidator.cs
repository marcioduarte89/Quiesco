namespace Availability.API.Models.Input.Room.Common
{
    using Availability.Common.Extensions;
    using FluentValidation;

    /// <summary>
    /// Create AddRoomAvailability validator
    /// </summary>
    public class PriceValidator : AbstractValidator<Price>
    {
        /// <summary>
        /// Validator constructor
        /// </summary>
        public PriceValidator()
        {
            RuleFor(x => x.Value)
                .GreaterThan(0)
                .WithMessage(x => "Price must be greater than 0");

            RuleFor(x => x.Date)
                .Must(DateExtensions.IsValid)
                .WithMessage(x => "Default price must be greater than 0");
        }
    }
}
