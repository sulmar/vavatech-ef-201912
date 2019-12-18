using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vavatech.WebApi.Models;

namespace Vavatech.WebApi.DbServices.Configurations
{
    internal class ProductConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductConfiguration()
        {
            Property(p => p.Color).HasMaxLength(20);
            Property(p => p.Name).HasMaxLength(50);
        }
    }
}
