namespace Products.API.Features.Properties.CreateProperty
{
    using Core.Enums;
    using MediatR;
    using Models.Output;

    /// <summary>
    /// Create property command
    /// </summary>
    public class CreatePropertyCommand : IRequest<Property>
    {
        /// <summary>
        /// Property type
        /// </summary>
        public PropertyTypes Type { get; private set; }

        /// <summary>
        /// Property Name
        /// </summary>
        public string Name { get; private set; }
    }
}
