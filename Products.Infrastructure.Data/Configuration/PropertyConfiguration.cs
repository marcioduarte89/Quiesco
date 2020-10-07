namespace Products.Infrastructure.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Core.Models;

    /// <summary>
    /// EF Property configuration
    /// </summary>
    public class PropertyConfiguration : IEntityTypeConfiguration<Property>
    {
        /// <summary>
        /// Configures Property
        /// </summary>
        /// <param name="builder">property builder</param>
        public void Configure(EntityTypeBuilder<Property> builder)
        {
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
