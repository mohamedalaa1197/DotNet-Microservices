using Dapper;
using dicount.gRPC.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Threading.Tasks;

namespace dicount.gRPC.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _configuration;

        public DiscountRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            using var connection = new NpgsqlConnection(
                                             _configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>
                (
                    "SELECT * FROM Coupon WHERE ProductName=@ProductName",
                    new { ProductName = productName }
                );

            if (coupon is null)
                return new Coupon { ProductName = "No discount", Amount = 0, Description = "no Description" };

            return coupon;
        }
        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(
                                            _configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var result = await connection.ExecuteAsync
                (
                    "INSERT INTO public.coupon (ProductName, Description, Amount) VALUES (@ProductName, @Description, @Amount)",
                    new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount }
                );

            return result > 0 ? true : false;
        }

        public async Task<bool> Deletediscount(string productName)
        {
            using var connection = new NpgsqlConnection(
                                                _configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var result = await connection.ExecuteAsync
                (
                    "DELETE FROM public.coupon WHERE ProductName=@ProductName",
                    new
                    {
                        ProductName = productName,
                    }
                );

            return result > 0 ? true : false;
        }



        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(
                                             _configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var result = await connection.ExecuteAsync
                (
                    "UPDATE public.coupon SET  productname =@ProductName, description =@Description, amount =@Amount WHERE Id=@Id",
                    new
                    {
                        ProductName = coupon.ProductName,
                        Description = coupon.Description,
                        Amount = coupon.Amount,
                        Id = coupon.Id
                    }
                );

            return result > 0 ? true : false;
        }
    }
}
