using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Vavatech.WebApi.IServices;
using Vavatech.WebApi.Models;

namespace Vavatech.WebApi.DbServices
{

    public class DbCustomerService : DbEntityService<Customer>, ICustomerService
    {
        public DbCustomerService(WarehouseContext context) : base(context)
        {
        }

        public IEnumerable<Customer> Get(string city, string street)
        {
            return entities.Where(c => c.HomeAddress.City.Contains(city) 
            && c.HomeAddress.Street.Contains(street)).ToList();

        }

        public Customer Get(string pesel)
        {
            return entities.SingleOrDefault(c => c.Pesel == pesel);
        }

        public bool TryAuthorize(string pesel, string hashPassword, out Customer customer)
        {
            customer = entities.FirstOrDefault(c => c.Pesel == pesel && c.HashPassword == hashPassword);

            return customer != null;
        }
    }
}
