namespace Availability.API.Features.BookSlots
{
    using MediatR;
    using Models.Output;

    /// <summary>
    /// Books slots for the room for a given time period
    /// </summary>
    public class BookSlotsCommand : IRequest<Room>
    {
    }
}
