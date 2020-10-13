using FluentValidation;

namespace Products.API.Models.Input.Property.Create
{
    /// <summary>
    /// Create property validator
    /// </summary>
    public class CreatePropertyValidator : AbstractValidator<CreateProperty>
    {
        /// <summary>
        /// Validator constructor
        /// </summary>
        public CreatePropertyValidator()
        {

            RuleFor(a => a.Name)
                .NotEmpty()
                .WithMessage("Property Name cannot be null");

            RuleFor(a => a.Type)
                .IsInEnum()
                .WithMessage("Property must have a valid type");
        }
    }
}
