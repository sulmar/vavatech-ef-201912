namespace Vavatech.WebApi.Models
{
    public class OrderDetail : EntityBase
    {
        public Item Item { get; set; }
        public short Quantity { get; set; }
        public decimal UnitPrice { get; set; }

    }
}
