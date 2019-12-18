using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Vavatech.WebApi.Models;

namespace Vavatech.WebApi.IServices
{
    public interface IProductService : IEntityService<Product>
    {
        IEnumerable<Product> Get(string color);
        IEnumerable<Product> Get(decimal from, decimal to);
        IEnumerable<Product> Get(ProductSearchCriteria criteria);
    }

    public class ProductSearchCriteria
    {
        public decimal? From { get; set; }
        public decimal? To { get; set; }
        public string Color { get; set; }
    }
}
