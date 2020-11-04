namespace Availability.API.Features.UpdatePrices
{
    using MediatR;
    using Models.Output;
    using System.Collections.Generic;
    using Price = Models.Input.Room.Common.Price;

    /// <summary>
    /// Update command
    /// </summary>
    public class UpdatePricesCommand : IRequest<Room>
    {
        /// <summary>
        /// Property Id
        /// </summary>
        public int PropertyId { get; set; }

        /// <summary>
        /// Room Id
        /// </summary>
        public int RoomId { get; set; }

        /// <summary>
        /// Updated room prices
        /// </summary>
        public IEnumerable<Price> Prices { get; set; }
    }
}
