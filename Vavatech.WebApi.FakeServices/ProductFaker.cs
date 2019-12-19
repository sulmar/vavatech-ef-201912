using Bogus;
using Vavatech.WebApi.Models;

namespace Vavatech.WebApi.FakeServices
{

    public class ProductFaker : Faker<Product>
    {
        public ProductFaker()
        {
            StrictMode(true);
            RuleFor(p => p.Id, f => f.IndexGlobal);
            RuleFor(p => p.Name, f => f.Commerce.ProductName());
            RuleFor(p => p.Color, f => f.Commerce.Color());
            RuleFor(p => p.UnitPrice, f => decimal.Parse(f.Commerce.Price()));
            RuleFor(p => p.Weight, f => f.Random.Float(100, 1000));
            RuleFor(p => p.IsRemoved, f => f.Random.Bool(0.2f));
        }
    }
}
