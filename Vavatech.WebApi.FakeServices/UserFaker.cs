using Bogus;
using Vavatech.WebApi.Models;

namespace Vavatech.WebApi.FakeServices
{
    public class UserFaker<TUser> : Faker<TUser>
        where TUser : User
    {
        public UserFaker()
            : base()
        {
            RuleFor(p => p.Id, f => f.IndexGlobal);
            RuleFor(p => p.FirstName, f => f.Person.FirstName);
            RuleFor(p => p.LastName, f => f.Person.LastName);
            RuleFor(p => p.Gender, f => (Gender)f.Person.Gender);
        } 
    }
}
