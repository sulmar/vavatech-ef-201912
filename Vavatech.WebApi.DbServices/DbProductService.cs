using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            Trace.WriteLine(context.Entry(entity).State);

            context.Products.Add(entity);

            Trace.WriteLine(context.Entry(entity).State);

            context.SaveChanges();

            Trace.WriteLine(context.Entry(entity).State);
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
            Product product = new Product { Id = id };
//context.Entry(product).State = System.Data.Entity.EntityState.Deleted;
  
            context.Products.Remove(product);
            context.SaveChanges();

            //Product product = Get(id);
            //context.Products.Remove(product);
            //context.SaveChanges();
        }

        public void Update(Product entity)
        {
            Trace.WriteLine(context.Entry(entity).State);

            context.Entry(entity).State = System.Data.Entity.EntityState.Modified;

            //Product product = Get(entity.Id);
            Trace.WriteLine(context.Entry(entity).State);

            //product.Name = entity.Name;
            //product.Color = entity.Color;

            Trace.WriteLine(context.Entry(entity).State);
            context.SaveChanges();
            Trace.WriteLine(context.Entry(entity).State);
        }

        public void Update(int id, string name)
        {
            Product entity = new Product { Id = id, Name = name };
            Trace.WriteLine(context.Entry(entity).State);

            context.Products.Attach(entity);
            Trace.WriteLine(context.Entry(entity).State);

            context.Entry(entity).Property(p => p.Name).IsModified = true;
            Trace.WriteLine(context.Entry(entity).State);

            context.SaveChanges();
            Trace.WriteLine(context.Entry(entity).State);

        }
    }
}
