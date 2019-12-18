using System;
using System.Collections.Generic;
using Vavatech.WebApi.Models;

namespace Vavatech.WebApi.IServices
{
    public interface ICustomerService : IEntityService<Customer>
    {  
        IEnumerable<Customer> Get(string city, string street);
        Customer Get(string pesel);

        bool TryAuthorize(string pesel, string hashPassword, out Customer customer);
        
    }



 

}
