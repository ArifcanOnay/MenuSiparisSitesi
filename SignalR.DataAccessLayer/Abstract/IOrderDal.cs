using SignalR.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.DataAccessLayer.Abstract
{
    public interface IOrderDal : IGenericDal<Order>
    {
        int TotalOrderCount();
        int ActiveOrderCount();
        decimal LastOrderPrice();
        decimal TodayTotalPrice();
        void UpdateOrderStatus(int orderId, string status);
        List<Order> GetOrdersByTableNumber(string tableNumber);
        Order GetLastOrderByTableNumber(string tableNumber);
    }
}
