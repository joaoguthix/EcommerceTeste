using Domain.Interfaces.IRepositorys;
using EntitieProduct;
using Infrastructure.Config;
using Infrastructure.Repository.Generic;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Repositories
{
    public class OrderRepository : RepositoryGeneric<Order>, IOrderRepository
    {
        private readonly DbContextOptions<SqlLiteDbContext> _OptionsBuilder;

        public OrderRepository()
        {
            _OptionsBuilder = new DbContextOptions<SqlLiteDbContext>();
        }

        public async Task<List<Order>> GetAll()
        {
            using (var data = new SqlLiteDbContext(_OptionsBuilder))
            {
                return await data.Orders.ToListAsync();
            }
        }
    }
}
