using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Entities
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {

        }
        public ShoppingCart(string UserName)
        {

        }
        public string UserName { get; set; }
        public ICollection<ShoppingCartItem> Items { get; set; }
        public double totalPrice
        {
            get
            {
                var totoal = TotalPrice(Items);
                return totoal;
            }
        }


        private double TotalPrice(ICollection<ShoppingCartItem> items)
        {
            if (items != null)
            {
                double total = 0;
                foreach (var item in items)
                {
                    total += item.Price * item.Quantity;
                }
                return total;
            }
            return 0;
        }
    }


}
