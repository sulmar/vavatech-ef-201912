using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vavatech.WebApi.IServices;
using Vavatech.WebApi.Models;

namespace Vavatech.WebApi.DbServices
{
    public class DbUserService : DbEntityService<User>, IUserService
    {
        public DbUserService(WarehouseContext context) : base(context)
        {
        }

        public IEnumerable<Employee> GetEmployees(string from, string to)
        {
            //var employees = context.Users
            //    .OfType<Employee>()
            //    .Where(u => u.Salary >= from && u.Salary <= to)
            //    .ToList();

            //return employees;



            // string sql = "select * from dbo.Users where Discriminator = 'Employee' and Salary < " + to;

            string sql = "select * from dbo.Users where Discriminator = 'Employee' and Salary < @to";

            SqlParameter salarySalary = new SqlParameter("@to", to);

            // context.Database.ExecuteSqlCommand("");

            return context.Database.SqlQuery<Employee>(sql, salarySalary);

        }
    }
}
