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
        public double totalPrice { get => TotalPrice(Items); }


        private double TotalPrice(ICollection<ShoppingCartItem> items)
        {
            double totalPrice = 0;
            foreach (var item in items)
            {
                totalPrice += item.Price * item.Quantity;
            }
            return totalPrice;
        }
    }


}
