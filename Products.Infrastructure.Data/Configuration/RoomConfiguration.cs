namespace Products.Infrastructure.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Core.Models;

    /// <summary>
    /// EF Room configuration
    /// </summary>
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        /// <summary>
        /// Configures Room
        /// </summary>
        /// <param name="builder">room builder</param>
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.ToTable(nameof(Room));
            builder.Property(x => x.NrOfOccupants).IsRequired();
            builder.Property(x => x.AccommodationType).HasConversion<int>();
            builder.Property(x => x.AccommodationType).IsRequired();
            builder.Property<int>("propertyId");
        }
    }
}
