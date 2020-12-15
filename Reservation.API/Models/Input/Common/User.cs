namespace Reservations.API.Models.Input.Common
{
    /// <summary>
    /// User definition required when a reservation is created
    /// </summary>
    public class User
    {        
        /// <summary>
        /// User email
        /// </summary>
        public string UserEmail { get; set; }

        /// <summary>
        /// User name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// User last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// User phone number
        /// </summary>
        public string PhoneNumber { get; set; }
    }
}
