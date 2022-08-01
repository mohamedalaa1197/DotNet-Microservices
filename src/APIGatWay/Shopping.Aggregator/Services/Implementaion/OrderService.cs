using System.Collections.Generic;
using System.Threading.Tasks;
using Shopping.Aggregator.Models;
using Shopping.Aggregator.Services.Contracts;

namespace Shopping.Aggregator.Services.Implementaion
{
    public class OrderService : IOrderService
    {
        public Task<IEnumerable<OrderResponseModel>> GetOrdersByUserName(string userName)
        {
            throw new System.NotImplementedException();
        }
    }
}