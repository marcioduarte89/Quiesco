namespace Products.API.Features.Properties.UpdateProperty
{
    using Core.Enums;
    using MediatR;
    using Models.Output;

    /// <summary>
    /// Update property command
    /// </summary>
    public class UpdatePropertyCommand : IRequest<Property>
    {
        /// <summary>
        /// Property Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Property type
        /// </summary>
        public PropertyTypes Type { get; set; }

        /// <summary>
        /// Property Name
        /// </summary>
        public string Name { get; set; }
    }
}
