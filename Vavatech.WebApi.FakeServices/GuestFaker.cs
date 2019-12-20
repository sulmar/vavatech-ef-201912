using Vavatech.WebApi.Models;

namespace Vavatech.WebApi.FakeServices
{
    public class GuestFaker : UserFaker<Guest>
    {
        public GuestFaker()
            : base()
        {
            RuleFor(p => p.Token, f => f.Lorem.Word());
        }
    }
}
