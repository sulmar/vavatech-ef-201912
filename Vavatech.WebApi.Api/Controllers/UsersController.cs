using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Vavatech.WebApi.IServices;
using Vavatech.WebApi.Models;

namespace Vavatech.WebApi.Api.Controllers
{
    public class UsersController : ApiController
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        //[HttpGet]
        //public IHttpActionResult Get()
        //{
        //    var users = userService.Get();

        //    return Ok(users);
        //}

        // api/users?from=0&to=1000
        [HttpGet]
        public IHttpActionResult GetEmployees(string from, string to)
        {
            var employees = userService.GetEmployees(from, to);

            return Ok(employees);

        }



        [HttpPost]
        public IHttpActionResult Post([FromBody] User user)
        {
            userService.Add(user);

            return CreatedAtRoute("DefaultApi", new { user.Id }, user);
        }
    }
}