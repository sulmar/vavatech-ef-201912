using System;
using System.Collections.Generic;
using System.Text;
using Vavatech.WebApi.Models;

namespace Vavatech.WebApi.IServices
{
    public interface IUserService : IEntityService<User>
    {
        IEnumerable<Employee> GetEmployees(string from, string to);


    }
}
