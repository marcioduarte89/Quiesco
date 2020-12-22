using Reservation.Common.Exceptions;

namespace Reservations.Core.Models
{
    public class User
    {
        /// <summary>
        /// User email
        /// </summary>
        public string UserEmail { get; private set; }

        /// <summary>
        /// User name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// User last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// User phone number
        /// </summary>
        public string PhoneNumber { get; private set; }

        public static User Create(string userEmail, string name, string phoneNumber)
        {
            Validate(userEmail, name, phoneNumber);

            return new User()
            {
                UserEmail = userEmail,
                Name = name,
                PhoneNumber = phoneNumber
            };
        }

        /// <summary>
        /// Validates the reservation details
        /// </summary>
        /// <param name="propertyId"></param>
        /// <param name="roomId"></param>
        /// <param name="checkIn"></param>
        /// <param name="checkOut"></param>
        /// <param name="nrOfOccupants"></param>
        private static void Validate(string userEmail, string Name, string phoneNumber)
        {
            void ThrowIfNullOrEmpty(string property)
            {
                if (string.IsNullOrEmpty(property))
                {
                    throw new ReservationException(ReservationException.INVALID_DATA, $"{nameof(property)} Cannot be null or empty");
                }
            }

            ThrowIfNullOrEmpty(userEmail);
            ThrowIfNullOrEmpty(Name);
            ThrowIfNullOrEmpty(phoneNumber);
        }
    }
}
