using System.Data.Entity;


namespace QuickSale.Infrastructure.Entities
{
    public class SalesDbContext : DbContext
    {
        public SalesDbContext() : base("name=SalesConnectionString")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}