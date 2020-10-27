namespace Availability.API.IntegrationTests.Builders.Fakers
{
    using System;
    using System.Linq;
    using Bogus;
    using Models.Input.Room.Create;

    public class CreateRoomFaker
    {
        private static int _roomId = 1;

        public static Faker<CreateRoom> Build(bool generatePrices, bool generateBookedSlots)
        {
            var fakeRoom = new Faker<CreateRoom>()
                .RuleFor(u => u.RoomId, f => _roomId++)
                .RuleFor(u => u.DefaultPrice, f => f.Random.Int(100, 200));

            if (generatePrices)
            {
                fakeRoom
                    .RuleFor(u => u.Prices, f => PricesFaker.Build().Generate(1));
            }

            if (generateBookedSlots)
            {
                fakeRoom.RuleFor(u => u.BookedSlots, f => new[] {int.Parse(DateTime.Now.AddMonths(1).ToString("ddMMyyyy"))}); // might want to open a pr in their repo to fix this (In the future is generating wrong datetimes)
            }

            return fakeRoom;
        }

        public static CreateRoom Generate(bool generatePrices, bool generateBookedSlots)
        {
            return Build(generatePrices, generateBookedSlots).Generate();
        }
    }
}
