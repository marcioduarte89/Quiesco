namespace Products.API.Features.Properties.Create.Profile
{
    using AutoMapper;
    using Core.Models;

    public class Output : Profile
    {
        public Output()
        {
            CreateMap<Property, Models.Output.Property>();
        }
    }
}
