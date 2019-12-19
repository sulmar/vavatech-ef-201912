using Bogus;
using Vavatech.WebApi.Models;

namespace Vavatech.WebApi.FakeServices
{
    public class AddressFaker : Faker<Address>
    {
        public AddressFaker()
        {
            RuleFor(p => p.City, f => f.Address.City());
            RuleFor(p => p.Street, f => f.Address.StreetName());
            RuleFor(p => p.PostCode, f => f.Address.ZipCode("#####"));
        }
    }
}
