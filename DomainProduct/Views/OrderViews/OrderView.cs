using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Views.OrderViews
{
    public class OrderAddView
    {
        public DateTime OrderDate { get; set; }
        public List<OrderItemView> OrderItem { get; set; }
    }

    public class OrderUpdateView
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderItemView> OrderItem { get; set; }
    }

    public class OrderView
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderItemView> OrderItem { get; set; }
    }

    public class OrderItemView
    {
        public int ProductCode { get; set; }
        public int Quantity { get; set; }
    }
}
