namespace Products.API.Features.Rooms.Create
{
    using Core.Enums;
    using MediatR;
    using Models.Output;

    public class CreateCommand : IRequest<Room>
    {
        public RoomTypes AccommodationType { get; set; }
        public int NrOfOccupants { get; set; }
}
}
