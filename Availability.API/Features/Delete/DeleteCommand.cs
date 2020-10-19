namespace Availability.API.Features.Delete {
    using MediatR;
    using Models.Output;

    /// <summary>
    /// Delete command
    /// </summary>
    public class DeleteCommand : IRequest<Room> {
    }
}
