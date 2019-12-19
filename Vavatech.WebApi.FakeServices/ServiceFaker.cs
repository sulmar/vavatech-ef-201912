using Bogus;
using System;
using Vavatech.WebApi.Models;

namespace Vavatech.WebApi.FakeServices
{
    public class ServiceFaker : Faker<Service>
    {
        public ServiceFaker()
        {
            StrictMode(true);
            RuleFor(p => p.Id, f => f.IndexGlobal);
            RuleFor(p => p.Name, f => f.Hacker.Verb());
            RuleFor(p => p.Cost, f => f.Finance.Amount(100, 1000));
            RuleFor(p => p.Duration, f => f.Date.Timespan(TimeSpan.FromHours(5)));
            RuleFor(p => p.IsRemoved, f => f.Random.Bool());
        }
    }
}
