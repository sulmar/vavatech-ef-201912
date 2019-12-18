using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vavatech.WebApi.Models;

namespace Vavatech.WebApi.DbServices.Configurations
{
    internal class CustomerConfiguration : EntityTypeConfiguration<Customer>
    {
        public CustomerConfiguration()
        {
            HasKey(p => p.Id);
            Property(p => p.FirstName).HasMaxLength(50).IsRequired();
            Property(p => p.MiddleName).HasMaxLength(50).IsOptional();
            Property(p => p.LastName).HasMaxLength(50).IsRequired();
            Property(p => p.Pesel).HasMaxLength(11).IsFixedLength().IsUnicode(false);
            Property(p => p.HashPassword).HasMaxLength(100);
            Property(p => p.DateOfBirth).HasColumnType("datetime2").HasPrecision(3);
            Property(p => p.Regon).HasMaxLength(14).IsFixedLength().IsUnicode(false);
        }
    }
}
