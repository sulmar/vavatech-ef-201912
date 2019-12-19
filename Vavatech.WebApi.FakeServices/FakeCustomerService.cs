using System;
using System.Collections.Generic;
using System.Linq;
using Vavatech.WebApi.IServices;
using Vavatech.WebApi.Models;

namespace Vavatech.WebApi.FakeServices
{

    public class FakeCustomerService : ICustomerService
    {
        private readonly ICollection<Customer> customers;
        private readonly CustomerFaker customerFaker;

        public FakeCustomerService()
        {
            customerFaker = new CustomerFaker(new AddressFaker());

            customers = customerFaker.Generate(100);
        }

        public void Add(Customer customer)
        {
            customers.Add(customer);
        }

        public bool Exists(int id)
        {
            return customers.Any(p=>p.Id == id);
        }

        public IEnumerable<Customer> Get()
        {
            return customers;
        }


        public IEnumerable<Customer> Get(string city, string street)
        {
            throw new NotImplementedException();
        }

        public Customer Get(int id)
        {
            return customers.SingleOrDefault(c => c.Id == id);
        }

        public Customer Get(string pesel)
        {
            return customers.SingleOrDefault(c => c.Pesel == pesel);
        }

        public void Remove(int id)
        {
            customers.Remove(Get(id));
        }

        public bool TryAuthorize(string pesel, string hashPassword, out Customer customer)
        {
            customer = customers.FirstOrDefault(c => c.Pesel == pesel && c.HashPassword == hashPassword);

            return customer != null;

        }

        public void Update(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
