using System;

namespace Vavatech.WebApi.Models
{
    public class Service : Item
    {
        public TimeSpan? Duration { get; set; }
        public decimal Cost { get; set; }
    }
}
