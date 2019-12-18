using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using Vavatech.WebApi.IServices;
using Vavatech.WebApi.Models;

namespace Vavatech.WebApi.FakeServices
{
    public class FakeEntityService<TEntity> : IEntityService<TEntity>
        where TEntity : EntityBase
    {
        protected readonly ICollection<TEntity> entities;
        private readonly Faker<TEntity> entityFaker;

        public FakeEntityService(Faker<TEntity> entityFaker)
        {
            this.entityFaker = entityFaker;

            entities = entityFaker.Generate(100);
        }

        public void Add(TEntity entity)
        {
            entities.Add(entity);
        }

        public bool Exists(int id)
        {
            return entities.Any(p => p.Id == id);
        }

        public IEnumerable<TEntity> Get()
        {
            return entities;
        }

        public TEntity Get(int id)
        {
            return entities.SingleOrDefault(e => e.Id == id);
        }

        public void Remove(int id)
        {
            entities.Remove(Get(id));
        }

        public void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }

    public class FakeCustomerService : ICustomerService
    {
        private readonly ICollection<Customer> customers;
        private readonly CustomerFaker customerFaker;

        public FakeCustomerService()
        {
            customerFaker = new CustomerFaker();

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
