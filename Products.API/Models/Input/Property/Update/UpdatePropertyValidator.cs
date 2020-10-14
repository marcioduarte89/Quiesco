using FluentValidation;

namespace Products.API.Models.Input.Property.Update
{
    /// <summary>
    /// Update property validator
    /// </summary>
    public class UpdatePropertyValidator : AbstractValidator<UpdateProperty>
    {
        /// <summary>
        /// Validator Constructor
        /// </summary>
        public UpdatePropertyValidator()
        {

            RuleFor(a => a.Name)
                .NotEmpty()
                .WithMessage("Property Name cannot be null");
        }
    }
}
