using FluentValidation;

namespace Availability.API.Models.Input.Room.Update {
    using Room.Update;

    /// <summary>
    /// Update Entity validator
    /// </summary>
    public class UpdateRoomAvailabilityValidator : AbstractValidator<UpdateRoomAvailability> {
        /// <summary>
        /// Validator constructor
        /// </summary>
        public UpdateRoomAvailabilityValidator() {
        }
    }
}
