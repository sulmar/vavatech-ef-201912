using System;
using System.Web.Http;
using System.Web.UI;
using Vavatech.WebApi.IServices;
using Vavatech.WebApi.Models;

namespace Vavatech.WebApi.Api.Controllers
{
    [RoutePrefix("api/products")]
    public class ProductsController : ApiController
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        //[HttpGet]
        //public IHttpActionResult Get()
        //{
        //    var products = productService.Get();

        //    return Ok(products);
        //}

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            var product = productService.Get(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        //[HttpGet]
        //[Route("{color:alpha}")]
        //public IHttpActionResult Get(string color)
        //{
        //    var products = productService.Get(color);

        //    return Ok(products);
        //}

        [HttpPost]
        public IHttpActionResult Post(Product product)
        {
            productService.Add(product);

            return CreatedAtRoute("DefaultApi", new { id = product.Id }, product);
        }

        // PUT /api/products/10

        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult Put(int id, Product product)
        {
            if (id != product.Id)
                return BadRequest();

            productService.Update(product);

            return Ok();
        }

        [HttpPatch]
        [Route("{id}")]
        public IHttpActionResult Patch(int id, [FromBody] string name)
        {
            productService.Update(id, name);

            return Ok();
        }

        [Route("{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (!productService.Exists(id))
                return NotFound();

            productService.Remove(id);

            return Ok();
        }

        //[HttpGet]
        //public IHttpActionResult Get(decimal from, decimal to)
        //{
        //    var products = productService.Get(from, to);

        //    return Ok(products);
        //}

        // api/products?color=Red&from=10&to=200

        [HttpGet]
        public IHttpActionResult Get([FromUri] ProductSearchCriteria criteria)
        {
            
            
            var products = productService.Get(criteria);

            var response = Ok(products);

            return response;
        }

        /// api/customers/10/products
        /// 

        [HttpGet]
        [Route("~/api/customers/{customerId}/products")]
        public IHttpActionResult GetByCustomer(int customerId)
        {
            throw new NotImplementedException();

            return Ok();
        }
    }
}