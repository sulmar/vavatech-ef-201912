using Vavatech.WebApi.IServices;
using Vavatech.WebApi.Models;

namespace Vavatech.WebApi.DbServices
{
    public class DbOrderService : DbEntityService<Order>, IOrderService
    {
        public DbOrderService(WarehouseContext context) : base(context)
        {
        }
    }
}
