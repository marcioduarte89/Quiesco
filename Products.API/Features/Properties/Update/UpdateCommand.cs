namespace Products.API.Features.Properties.Update
{
    using Core.Enums;
    using MediatR;
    using Models.Output;

    public class UpdateCommand : IRequest<Property>
    {
        public int Id { get; set; }
        public PropertyTypes Type { get; set; }
        public string Name { get; set; }
    }
}
