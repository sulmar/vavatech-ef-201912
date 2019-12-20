using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Vavatech.WebApi.IServices;
using Vavatech.WebApi.Models;
using System.Data.Entity;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics;

namespace Vavatech.WebApi.DbServices
{
    public class DbOrderService : DbEntityService<Order>, IOrderService
    {
        public DbOrderService(WarehouseContext context) : base(context)
        {
        }

        public override void Add(Order entity)
        {
            context.Orders.Add(entity);

            context.Entry(entity.Customer).State = System.Data.Entity.EntityState.Unchanged;

            foreach (var detail in entity.Details)
            {
                context.Entry(detail.Item).State = System.Data.Entity.EntityState.Unchanged;
            }

#if DEBUG
            // Pobiera graf wszystkich śledzonych obiektów
            var entries = context.ChangeTracker.Entries();
#endif

            context.SaveChanges();
        }

        // eadger loading
        //public override IEnumerable<Order> Get()
        //{
        //    return entities
        //        .Include(p => p.Customer)
        //        // .Include("Details.Item")
        //        .Include(p => p.Details.Select(d => d.Item))
        //        .ToList();
        //}

        // explicit loading
        public override IEnumerable<Order> Get()
        {
            var orders = context.Orders
                .ToList();


           // var items = orders.SelectMany(o => o.Details).Select(d=>d.Item).ToList();


            foreach (var order in orders)
            {
                context.Entry(order).Reference(p => p.Customer).Load();

                Trace.WriteLine(order.Customer.FirstName);

                context.Entry(order).Collection(p => p.Details).Load();

                foreach (var detail in order.Details)
                {
                    context.Entry(detail).Reference(p => p.Item).Load();

                    Trace.WriteLine(detail.Item.Name);
                }
            }

            return orders;
        }

        // lazy loading
        //public override IEnumerable<Order> Get()
        //{
        //    var orders = context.Orders.ToList();

        //    foreach (var order in orders)
        //    {
        //        Trace.WriteLine(order.Customer.FirstName);

        //        foreach (var detail in order.Details)
        //        {
        //            Trace.WriteLine(detail.Item.Name);
        //        }
        //    }

        //    return orders;

        //}
    }
}
