namespace Products.Infrastructure.Data
{
    using Microsoft.EntityFrameworkCore;
    using Core.Models;
    using System.Reflection;
    public class ProductsContext : DbContext
    {
        public DbSet<Property> Properties { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public ProductsContext(DbContextOptions<ProductsContext> contextOptions) : base(contextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
