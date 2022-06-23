using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Presestiance
{
    public class OrderContextSeeding
    {
        public static async Task SeedContext(SQLOrderContext context, ILogger<OrderContextSeeding> logger)
        {
            if (!context.Orders.Any())
            {
                context.Orders.AddRange(GetPreconfiguredOrders());
                await context.SaveChangesAsync();
                logger.LogInformation("Seed the Database");
            }
        }

        private static IEnumerable<Order> GetPreconfiguredOrders()
        {
            return new List<Order>
            {
                new Order() {UserName = "Alaa", FirstName = "Mohamed", LastName = "Alaa", EmailAddress = "mohamedalaasaad12@outlook.com", AddressLine = "Menofia", Country = "Egypt", TotalPrice = 350 }
            };
        }
    }
}
