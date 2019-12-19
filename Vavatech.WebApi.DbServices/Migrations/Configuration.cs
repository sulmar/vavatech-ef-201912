namespace Vavatech.WebApi.DbServices.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Vavatech.WebApi.FakeServices;
    using Vavatech.WebApi.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Vavatech.WebApi.DbServices.WarehouseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Vavatech.WebApi.DbServices.WarehouseContext";
        }

        protected override void Seed(Vavatech.WebApi.DbServices.WarehouseContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            ProductFaker productFaker = new ProductFaker();
            ServiceFaker serviceFaker = new ServiceFaker();
            CustomerFaker customerFaker = new CustomerFaker(new AddressFaker());

            Product[] products = productFaker.Generate(10).ToArray();

            context.Products.AddOrUpdate(p => p.Id, products);

            Service[] services = serviceFaker.Generate(20).ToArray();

            context.Services.AddOrUpdate(p => p.Id, services);

            context.Customers.AddOrUpdate(p => p.Id, customerFaker.Generate(50).ToArray());
        }
    }
}
