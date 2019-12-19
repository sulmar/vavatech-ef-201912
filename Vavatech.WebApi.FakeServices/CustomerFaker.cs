using Bogus;
using System;
using System.Collections.Generic;
using System.Text;
using Vavatech.WebApi.Models;

namespace Vavatech.WebApi.FakeServices
{

    public class CustomerFaker : Faker<Customer>
    {
        public CustomerFaker(Faker<Address> addressFaker)
        {
            StrictMode(true);
            UseSeed(1);
            RuleFor(p => p.Id, f => f.IndexFaker);
            RuleFor(p => p.FirstName, f => f.Person.FirstName);
            RuleFor(p => p.LastName, f => f.Person.LastName);
            RuleFor(p => p.Gender, f => (Gender) f.Person.Gender);
            RuleFor(p => p.Salary, f => f.Finance.Amount(100, 1000));
            RuleFor(p => p.IsRemoved, f => f.Random.Bool(0.3f));
            RuleFor(p => p.Pesel, f => f.Random.ReplaceNumbers("###########"));
            RuleFor(p => p.HashPassword, f => f.Internet.Password());
            Ignore(p => p.DateOfBirth);
            RuleFor(p => p.HomeAddress, f => addressFaker.Generate());
            RuleFor(p => p.InvoiceAddress, f => addressFaker.Generate());
            Ignore(p => p.MiddleName);
            RuleFor(p => p.Regon, f => f.Random.ReplaceNumbers(new string('#', 14)));

        }

      
    }
}
