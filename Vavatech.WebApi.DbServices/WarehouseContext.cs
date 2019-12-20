using System.Data.Entity;
using System.Diagnostics;
using Vavatech.WebApi.DbServices.Configurations;
using Vavatech.WebApi.DbServices.Conventions;
using Vavatech.WebApi.Models;

namespace Vavatech.WebApi.DbServices
{
    // Install-Package EntityFramework
    public class WarehouseContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }

        public WarehouseContext()
            : base("MyConnection")
        {
            // domyślne ustawienia:
            this.Configuration.ProxyCreationEnabled = false;
            this.Configuration.LazyLoadingEnabled = false;

            Trace.WriteLine(this.Configuration.ProxyCreationEnabled);
            Trace.WriteLine(this.Configuration.LazyLoadingEnabled);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {           
            modelBuilder.Configurations
                .Add(new CustomerConfiguration())
                .Add(new ProductConfiguration());

            modelBuilder.Conventions.Add(new DateTime2Convention());
            modelBuilder.Conventions.Add(new AddressConvention());


            base.OnModelCreating(modelBuilder);
        }
    }
}
