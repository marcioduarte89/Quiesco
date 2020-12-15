namespace Reservations.API.Models.Input.Common
{
    using FluentValidation;

    /// <summary>
    /// Create Reservation validator
    /// </summary>
    public class UserValidator : AbstractValidator<User>
    {
        /// <summary>
        /// Validator constructor
        /// </summary>
        public UserValidator()
        {
            RuleFor(a => a.UserEmail)
                    .NotEmpty()
                    .WithMessage(x => "User email must be set");

            RuleFor(a => a.Name)
                .NotEmpty()
                .WithMessage(x => "User name must be set");

            RuleFor(a => a.LastName)
                .NotEmpty()
                .WithMessage(x => "User last name must be set");

            RuleFor(a => a.PhoneNumber)
                .NotEmpty()
                .WithMessage(x => "User last name must be set");
        }
    }
}
