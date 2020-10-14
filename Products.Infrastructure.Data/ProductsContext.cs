namespace Products.Infrastructure.Data
{
    using Microsoft.EntityFrameworkCore;
    using Core.Models;
    using System.Reflection;

    /// <summary>
    /// Products Context
    /// </summary>
    public class ProductsContext : DbContext
    {
        /// <summary>
        /// Property Db set
        /// </summary>
        public DbSet<Property> Properties { get; set; }

        /// <summary>
        /// Rooms Db set
        /// </summary>
        public DbSet<Room> Rooms { get; set; }

        public ProductsContext(DbContextOptions<ProductsContext> contextOptions) : base(contextOptions)
        {

        }

        /// <summary>
        /// Configure the model that was discovered by convention from the entity types exposed in Microsoft.EntityFrameworkCore.DbSet`1 properties.
        /// </summary>
        /// <param name="builder">
        /// The builder being used to construct the model for this context. Databases (and//     other extensions)
        /// typically define extension methods on this object that allow you to configure aspects of the model that are specific to a given database.
        /// </param>
        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
