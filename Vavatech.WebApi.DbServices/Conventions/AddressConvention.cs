using System.Data.Entity.ModelConfiguration.Conventions;

namespace Vavatech.WebApi.DbServices.Conventions
{
    internal class AddressConvention : Convention
    {
        public AddressConvention()
        {
            this.Properties().Where(p => p.Name.Contains("City"))
                .Configure(c => c.HasMaxLength(100));

            this.Properties().Where(p => p.Name.Contains("Street"))
              .Configure(c => c.HasMaxLength(100));

            this.Properties().Where(p => p.Name.Contains("PostCode"))
              .Configure(c => c.HasMaxLength(5).IsFixedLength().IsUnicode(false));
        }
    }
}
