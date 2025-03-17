using DomainProduct.Interfaces.IRepositorys;
using EntitieProduct;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces.IRepositorys
{
    public interface IOrderItemRepository : IGenericRepository<OrderItem>
    {
        Task AddRange(IEnumerable<OrderItem> orderItems);
    }
}
