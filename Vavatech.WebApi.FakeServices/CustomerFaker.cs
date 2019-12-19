using Bogus;
using System;
using System.Collections.Generic;
using System.Text;
using Vavatech.WebApi.Models;

namespace Vavatech.WebApi.FakeServices
{
    public class CustomerFaker : Faker<Customer>
    {
        public CustomerFaker()
        {
            StrictMode(true);
            UseSeed(1);
            RuleFor(p => p.Id, f => f.IndexFaker);
            RuleFor(p => p.FirstName, f => f.Person.FirstName);
            RuleFor(p => p.LastName, f => f.Person.LastName);
            RuleFor(p => p.Gender, f => (Gender) f.Person.Gender);
            RuleFor(p => p.Salary, f => f.Finance.Amount(100, 1000));
            RuleFor(p => p.IsRemoved, f => f.Random.Bool(0.3f));
            RuleFor(p => p.Pesel, f => f.Commerce.Ean13());
            RuleFor(p => p.HashPassword, f => f.Internet.Password());
            Ignore(p => p.DateOfBirth);
            Ignore(p => p.HomeAddress);
            Ignore(p => p.InvoiceAddress);
            Ignore(p => p.MiddleName);
            Ignore(p => p.Regon);

        }

      
    }
}
