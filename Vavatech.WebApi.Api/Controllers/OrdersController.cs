using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Vavatech.WebApi.FakeServices;
using Vavatech.WebApi.IServices;
using Vavatech.WebApi.Models;

namespace Vavatech.WebApi.Api.Controllers
{
    [RoutePrefix("api/orders")]
    public class OrderController : ApiController
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
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
            CustomerFaker customerFaker = new CustomerFaker(new AddressFaker());

            order = new Order
            {
                Customer = customerFaker.Generate(),
                Details = new List<OrderDetail>
                { 
                    new OrderDetail { Item = new Product { Id = 1 }, Quantity = 3, UnitPrice = 2 },
                    new OrderDetail { Item = new Product { Id = 2 }, Quantity = 5, UnitPrice = 10 },

                }
            };

            orderService.Add(order);
            return CreatedAtRoute("DefaultApi", new { Id = order.Id }, order);
        }
    }
        
}