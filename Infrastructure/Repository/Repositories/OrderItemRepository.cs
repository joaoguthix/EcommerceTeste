using Domain.Interfaces.IRepositorys;
using EntitieProduct;
using Infrastructure.Config;
using Infrastructure.Repository.Generic;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Repositories
{
    public class OrderItemRepository : RepositoryGeneric<OrderItem>, IOrderItemRepository
    {
        private readonly DbContextOptions<SqlLiteDbContext> _OptionsBuilder;

        public OrderItemRepository()
        {
            _OptionsBuilder = new DbContextOptions<SqlLiteDbContext>();
        }

        public async Task AddRange(IEnumerable<OrderItem> orderItems)
        {
            using (var data = new SqlLiteDbContext(_OptionsBuilder))
            {
                await data.OrderItems.AddRangeAsync(orderItems);
                await data.SaveChangesAsync();
            }
        }
    }
}
