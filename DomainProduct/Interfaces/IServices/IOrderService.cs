using Domain.Views.OrderViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.IServices
{
    public interface IOrderService
    {
        Task<OrderView> Add(OrderAddView order);
        Task<OrderView> Update(OrderUpdateView order);
        Task<List<OrderView>> GetAll();
    }
}
