using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Products.Core.Models;

namespace Products.Infrastructure.Data.Configuration {
    public class PropertyConfiguration : IEntityTypeConfiguration<Property> {
        public void Configure(EntityTypeBuilder<Property> builder) {
            builder.ToTable(nameof(Property));

            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Type).HasConversion<int>();
            var metadata = builder.Metadata.FindNavigation(nameof(Property.Rooms));
            metadata.SetField("_rooms");
            metadata.SetPropertyAccessMode(PropertyAccessMode.Field);
            builder.HasMany(x => x.Rooms).WithOne(x => x.Property).HasForeignKey("propertyId");
        }
    }
}
