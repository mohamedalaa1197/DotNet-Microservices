using dicount.gRPC.Entities;
using System.Threading.Tasks;

namespace dicount.gRPC.Repositories
{
    public interface IDiscountRepository
    {
        public Task<Coupon> GetDiscount(string productName);
        public Task<bool> CreateDiscount(Coupon coupon);
        public Task<bool> UpdateDiscount(Coupon coupon);
        public Task<bool> Deletediscount(string productName);
    }
}