using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vavatech.WebApi.IServices;
using Vavatech.WebApi.Models;

namespace Vavatech.WebApi.DbServices
{

    public class DbProductService : IProductService
    {
        private readonly WarehouseContext context;

        public DbProductService(WarehouseContext context)
        {
            this.context = context;
        }

        public void Add(Product entity)
        {
            context.Products.Add(entity);
            context.SaveChanges();
        }

        public bool Exists(int id)
        {
            return context.Products.Any(p => p.Id == id);
        }

        public IEnumerable<Product> Get(string color)
        {
            return context.Products.Where(p => p.Color == color).ToList();
        }

        public IEnumerable<Product> Get(decimal from, decimal to)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> Get(ProductSearchCriteria criteria)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> Get()
        {
            return context.Products.ToList();
        }

        public Product Get(int id)
        {
            return context.Products.Find(id);
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
