namespace Availability.API.IntegrationTests.Builders.Fakers
{
    using System;
    using Bogus;
    using Models.Input.Room.Common;

    public class PricesFaker
    {
        public static Faker<Price> Build()
        {
            var fakePrice = new Faker<Price>()
                .RuleFor(u => u.Date, f => int.Parse(DateTime.Now.AddDays(1).ToString("ddMMyyyy")))
                .RuleFor(u => u.Value, f => f.Random.Int(100, 200));

            return fakePrice;
        }

        public static Price Generate()
        {
            return PricesFaker.Build().Generate();
        }
    }
}
