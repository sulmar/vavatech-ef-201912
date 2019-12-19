using Bogus;
using System.Collections.Generic;
using System.Linq;
using Vavatech.WebApi.IServices;
using Vavatech.WebApi.Models;

namespace Vavatech.WebApi.FakeServices
{
    public class FakeProductService : FakeEntityService<Product>, IProductService
    {
        public FakeProductService(Faker<Product> entityFaker) : base(entityFaker)
        {
        }

        public IEnumerable<Product> Get(string color)
        {
            return entities
                .Where(e => e.Color == color)
                .ToList();
        }

        public IEnumerable<Product> Get(decimal from, decimal to)
        {
            return entities
                .Where(e => e.UnitPrice >= from && e.UnitPrice <= to)
                .ToList();

        }

        public IEnumerable<Product> Get(ProductSearchCriteria criteria)
        {
            var results = entities.AsQueryable();

            if (criteria.From.HasValue)
            {
                results = results.Where(p => p.UnitPrice >= criteria.From);
            }

            if (criteria.To.HasValue)
            {
                results = results.Where(p => p.UnitPrice <= criteria.To);
            }

            if (!string.IsNullOrEmpty(criteria.Color))
            {
                results = results.Where(p => p.Color == criteria.Color);
            }

            return results.ToList();
        }

        public void Update(int id, string name)
        {
            throw new System.NotImplementedException();
        }
    }
}
