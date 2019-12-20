using Vavatech.WebApi.IServices;
using Vavatech.WebApi.Models;

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
    }
}
