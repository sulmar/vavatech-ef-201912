using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.UI.WebControls;
using Vavatech.WebApi.FakeServices;
using Vavatech.WebApi.IServices;
using Vavatech.WebApi.Models;

namespace Vavatech.WebApi.Api.Controllers
{
  
    public class OrdersController : ApiController
    {
        private readonly IOrderService orderService;

        public OrdersController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var orders = orderService.Get();
            return Ok(orders);
        }

        [HttpPost]
        public IHttpActionResult Post(Order order)
        {
            AddressFaker addressFaker = new AddressFaker();
            CustomerFaker customerFaker = new CustomerFaker(addressFaker);

            order = new Order
            {
                Customer = new Customer { Id = 4 },
                Details = new List<OrderDetail>
                {
                    new OrderDetail { Item = new Product { Id = 1 }, Quantity = 3, UnitPrice = 2 },
                    new OrderDetail { Item = new Product { Id = 2 }, Quantity = 5, UnitPrice = 10 },

                },
                ShipAddress = addressFaker.Generate()
            };

            orderService.Add(order);
            return CreatedAtRoute("DefaultApi", new { Id = order.Id }, order);
        }
    }
        
}