using System.Threading.Tasks;
using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.Services.Contracts
{
    public interface IBasketService
    {
        public Task<BasketModel> GetBasket(string userName);
    }
}