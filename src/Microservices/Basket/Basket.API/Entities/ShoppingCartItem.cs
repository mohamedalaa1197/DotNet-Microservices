using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Entities
{
    public class ShoppingCartItem
    {
        public string ProductName { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public string Color { get; set; }
        public double Price { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}
