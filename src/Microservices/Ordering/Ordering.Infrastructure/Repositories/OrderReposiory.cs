using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts.Presistence;
using Ordering.Domain.Entities;
using Ordering.Infrastructure.Presestiance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Repositories
{
    public class OrderReposiory : RepositoryBase<Order>, IOrderRepository
    {
        public OrderReposiory(SQLOrderContext context) : base(context)
        {
        }


        public async Task<IEnumerable<Order>> GetOrderByUserName(string userName)
        {
            var orders = await _context.Orders.Where(o => o.UserName.ToLower() == userName.ToLower()).ToListAsync();
            return orders;
        }
    }
}
