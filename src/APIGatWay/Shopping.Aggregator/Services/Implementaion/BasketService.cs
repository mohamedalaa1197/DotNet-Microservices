using System.Threading.Tasks;
using Shopping.Aggregator.Models;
using Shopping.Aggregator.Services.Contracts;

namespace Shopping.Aggregator.Services.Implementaion
{
    public class BasketService : IBasketService
    {
        public Task<BasketModel> GetBasket(string userName)
        {
            throw new System.NotImplementedException();
        }
    }
}