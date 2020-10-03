using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Products.Core.Models;

namespace Products.Infrastructure.Data.Configuration {
    public class RoomConfiguration : IEntityTypeConfiguration<Room> {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.ToTable(nameof(Room));
            builder.Property(x => x.NrOfOccupants).IsRequired();
            builder.Property(x => x.AccommodationType).HasConversion<int>();
            builder.Property(x => x.AccommodationType).IsRequired();
            builder.Property<int>("propertyId");
            //builder.Metadata.FindNavigation("_property").SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
