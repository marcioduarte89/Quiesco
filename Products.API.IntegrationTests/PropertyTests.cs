namespace Products.API.IntegrationTests
{
    using Builders;
    using Models.Input.Property.Update;
    using NUnit.Framework;
    using System.Net;
    using System.Threading.Tasks;

    [TestFixture]
    [Category("Integration")]
    public class PropertyTests : PropertyBuilder
    {
        //[Test]
        //public async Task CreateProperty_WhenProvidingAllValidDetails_ShouldCreateProperty()
        //{
        //    var property = CreatePropertyFaker.Generate();
        //    var response = await CreateProperty(property);

        //    Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        //    Assert.IsNotNull(response.Property);
        //    Assert.AreEqual(property.Name, response.Property.Name);
        //    Assert.AreEqual(property.Type, response.Property.Type);
        //}

        //[Test]
        //public async Task UpdateProperty_WhenUpdatingName_ShouldUpdateProperty()
        //{
        //    var property = CreatePropertyFaker.Generate();
        //    var createPropertyResponse = await CreateProperty(property);

        //    var updateProperty = new UpdateProperty() { Name = "Some updated name" };

        //    var response = await UpdateProperty(createPropertyResponse.Property.Id, updateProperty);

        //    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        //    Assert.IsNotNull(response.Property);
        //    Assert.AreEqual(updateProperty.Name, response.Property.Name);
        //    Assert.AreEqual(createPropertyResponse.Property.Type, response.Property.Type);
        //}

        //[Test]
        //public async Task UpdateProperty_WhenNotExistingProperty_ReturnsNotFound()
        //{
        //    var property = CreatePropertyFaker.Generate();
        //    var createPropertyResponse = await CreateProperty(property);

        //    var updateProperty = new UpdateProperty() { Name = "Some updated name" };

        //    var response = await UpdateProperty(1111, updateProperty);

        //    Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        //}
    }
}
