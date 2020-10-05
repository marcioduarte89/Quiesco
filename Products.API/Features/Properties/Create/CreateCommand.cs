namespace Products.API.Features.Properties.Create
{
    using Core.Enums;
    using MediatR;
    using Models.Output;

    public class CreateCommand : IRequest<Property>
    {
        public PropertyTypes Type { get; private set; }
        public string Name { get; private set; }
    }
}
