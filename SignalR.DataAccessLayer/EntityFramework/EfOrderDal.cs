using SignalR.DataAccessLayer.Abstract;
using SignalR.DataAccessLayer.Concrete;
using SignalR.DataAccessLayer.Repositories;
using SignalR.EntityLayer.Entities;
using SignalR.EntiyLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.DataAccessLayer.EntityFramework
{
    public class EfOrderDal : GenericRepository<Order>, IOrderDal
    {
        public EfOrderDal(SignalRContext context) : base(context)
        {
        }

        public int ActiveOrderCount()
        {
            using var context = new SignalRContext();
            return context.Orders.Where(x => x.Description == "Müşteri Masada").Count();
        }

        public decimal LastOrderPrice()
        {
            using var context = new SignalRContext();
            return context.Orders.OrderByDescending(x => x.OrderID).Take(1).Select(y=>y.TotalPrice).FirstOrDefault();
        }

        public decimal TodayTotalPrice()
        {
            return 0;
        }

        public int TotalOrderCount()
        {
            using var context = new SignalRContext();
            return context.Orders.Count();
        }

        public void UpdateOrderStatus(int orderId, string status)
        {
            using var context = new SignalRContext();
            var order = context.Orders.Find(orderId);
            if (order != null)
            {
                order.Description = status;
                context.SaveChanges();
            }
        }

        public List<Order> GetOrdersByTableNumber(string tableNumber)
        {
            using var context = new SignalRContext();
            return context.Orders.Where(x => x.TableNumber == tableNumber).OrderByDescending(x => x.OrderDate).ToList();
        }

        public Order GetLastOrderByTableNumber(string tableNumber)
        {
            using var context = new SignalRContext();
            return context.Orders.Where(x => x.TableNumber == tableNumber).OrderByDescending(x => x.OrderID).FirstOrDefault();
        }
    }
}
