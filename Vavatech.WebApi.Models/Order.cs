using System;
using System.Collections.Generic;
using System.Text;

namespace Vavatech.WebApi.Models
{
    public class Order : EntityBase
    {
        public DateTime OrderDate { get; set; }
        public Customer Customer { get; set; }
        public OrderStatus Status { get; set; }
        public Address ShipAddress { get; set; }
        public ICollection<OrderDetail> Details { get; set; }

        public Order()
        {
            Details = new HashSet<OrderDetail>();
            
            Status = OrderStatus.Created;
            OrderDate = DateTime.Now;
        }
    }
}
