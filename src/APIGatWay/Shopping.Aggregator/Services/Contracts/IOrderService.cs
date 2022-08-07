using System.Collections.Generic;
using System.Threading.Tasks;
using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.Services.Contracts
{
    public interface IOrderService
    {
        public Task<IEnumerable<OrderResponseModel>> GetOrdersByUserName(string userName);
    }
}