using System.Collections.Generic;

namespace Shopping.Aggregator.Models
{
    public class BasketModel
    {
        public string UserName { get; set; }
        public List<BasketItemExtendModel> Items { get; set; } = new List<BasketItemExtendModel>();
        public decimal TotalPrice { get; set; }
    }
}