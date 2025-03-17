using DomainProduct.Interfaces.IRepositorys;
using EntitieProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.IRepositorys
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<List<Order>> GetAll();
    }
}
