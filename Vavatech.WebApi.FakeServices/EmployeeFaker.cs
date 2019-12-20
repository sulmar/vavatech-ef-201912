using Vavatech.WebApi.Models;

namespace Vavatech.WebApi.FakeServices
{
    public class EmployeeFaker : UserFaker<Employee>
    {
        public EmployeeFaker()
            : base()
        {
            RuleFor(p => p.Salary, f => f.Finance.Amount(100, 500));
            RuleFor(p => p.Department, f => f.Commerce.Department());
        }
    }
}
