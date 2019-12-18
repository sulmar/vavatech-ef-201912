namespace Vavatech.WebApi.Models
{
    public abstract class Item : EntityBase
    {
        public string Name { get; set; }
        public bool IsRemoved { get; set; }
    }
}
