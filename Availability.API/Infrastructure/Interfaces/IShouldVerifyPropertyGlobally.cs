namespace Availability.API.Infrastructure.Interfaces
{
    /// <summary>
    /// Interface used when we need to check if the property exists globally
    /// </summary>
    public interface IShouldVerifyPropertyGlobally
    {
        /// <summary>
        /// Property Id where the Room belongs to
        /// </summary>
        int PropertyId { get; set; }

        /// <summary>
        /// Room id
        /// </summary>
        int RoomId { get; set; }
    }
}
