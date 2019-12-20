using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using Vavatech.WebApi.FakeServices;
using Vavatech.WebApi.IServices;
using Vavatech.WebApi.Models;

namespace Vavatech.WebApi.Api.Controllers
{
  //  [Authorize(Roles = "admin")]
    [RoutePrefix("api/customers")]
    public class CustomersController : ApiController
    {
        private readonly ICustomerService customerService;

        //public CustomersController()
        //    : this(new FakeCustomerService())
        //{

        //}

        public CustomersController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        [HttpGet]
        [Authorize]
        public IHttpActionResult Get()
        {
            //if (!this.User.Identity.IsAuthenticated)
            //{
            //    return Unauthorized();
            //}

            ClaimsIdentity identity = (ClaimsIdentity)this.User.Identity;

            var email = identity.FindFirst(ClaimTypes.Email).Value;

            var customers = customerService.Get();

            return Ok(customers);
        }


        // constraint
        [HttpGet]
        [Route("{id:int}", Order = 2)]
        public IHttpActionResult Get(int id)
        {
            var customer = customerService.Get(id);

            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        [HttpGet]
        [Route("{number:pesel}", Order = 1)]
        public IHttpActionResult Get(string number)
        {
            var customer = customerService.Get(number);

            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        [HttpGet]
        public IHttpActionResult Get(string city, string street)
        {
            var customers = customerService.Get(city, street);

            return Ok(customers);
        }

        [HttpPost]
        public IHttpActionResult Post(Customer customer)
        {
            customerService.Add(customer);

            return CreatedAtRoute("DefaultApi", new { id = customer.Id }, customer);
        }

        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult Put(int id, Customer customer)
        {
            if (id != customer.Id)
                return BadRequest();

            customerService.Update(customer);

            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            customerService.Remove(id);

            return Ok();
        }

        [HttpHead]
        [Route("{id:int}")]
        public IHttpActionResult Exists(int id)
        {
            if (customerService.Exists(id))
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

       

        

       
    }
}