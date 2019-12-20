using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Vavatech.WebApi.Models
{
 
    [KnownType(typeof(Product))]
    [KnownType(typeof(Service))]
    public abstract class Item : EntityBase
    {
        public string Name { get; set; }
        public bool IsRemoved { get; set; }
    }
}
