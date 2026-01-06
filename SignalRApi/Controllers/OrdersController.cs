using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalR.BusinessLayer.Abstract;
using SignalR.EntityLayer.Entities;
using SignalRApi.Hubs;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IBasketService _basketService;
        private readonly IMenuTableService _menuTableService;
        private readonly IHubContext<SignalRHub> _hubContext;

        public OrdersController(IOrderService orderService, IBasketService basketService, IMenuTableService menuTableService, IHubContext<SignalRHub> hubContext)
        {
            _orderService = orderService;
            _basketService = basketService;
            _menuTableService = menuTableService;
            _hubContext = hubContext;
        }

        [HttpGet("TotalOrderCount")]
        public IActionResult TotalOrderCount()
        {
            return Ok(_orderService.TTotalOrderCount());
        }

        [HttpGet("ActiveOrderCount")]
        public IActionResult ActiveOrderCount()
        {
            return Ok(_orderService.TActiveOrderCount());
        }

        [HttpGet("LastOrderPrice")]
        public IActionResult LastOrderPrice()
        {
            return Ok(_orderService.TLastOrderPrice());
        }
        [HttpGet("TodayTotalPrice")]
        public IActionResult TodayTotalPrice()
        {
            return Ok(_orderService.TTodayTotalPrice());    
        }

        [HttpGet]
        public IActionResult GetAllOrders()
        {
            var orders = _orderService.TGetListAll();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public IActionResult GetOrderById(int id)
        {
            var order = _orderService.TGetByID(id);
            return Ok(order);
        }

        [HttpGet("GetOrdersByTableNumber")]
        public IActionResult GetOrdersByTableNumber(string tableNumber)
        {
            var orders = _orderService.TGetOrdersByTableNumber(tableNumber);
            return Ok(orders);
        }

        [HttpGet("GetLastOrderByTableNumber")]
        public IActionResult GetLastOrderByTableNumber(string tableNumber)
        {
            var order = _orderService.TGetLastOrderByTableNumber(tableNumber);
            return Ok(order);
        }

        [HttpPost("CreateOrderFromBasket")]
        public IActionResult CreateOrderFromBasket(int menuTableId)
        {
            // Sepetteki ürünleri al
            var basketItems = _basketService.TGetBasketByMenuTableNumber(menuTableId);
            
            if (basketItems == null || !basketItems.Any())
            {
                return BadRequest("Sepet boş!");
            }

            // Toplam fiyatı hesapla
            decimal totalPrice = basketItems.Sum(x => x.TotalPrice);

            // Order oluştur
            var order = new Order
            {
                TableNumber = "Masa " + menuTableId,
                MenuTableID = menuTableId,
                Description = "Sipariş Alındı",
                OrderDate = DateTime.Now,
                TotalPrice = totalPrice
            };

            _orderService.TAdd(order);

            // Masayı dolu yap
            _menuTableService.TChangeMenuTableStatusToTrue(menuTableId);

            // Sepeti temizle
            foreach (var item in basketItems)
            {
                _basketService.TDelete(item);
            }

            return Ok(new { message = "Sipariş başarıyla oluşturuldu!", orderId = order.OrderID });
        }

        [HttpPut("UpdateOrderStatus/{orderId}")]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, [FromBody] string status)
        {
            // Siparişi al
            var order = _orderService.TGetByID(orderId);
            if (order == null)
            {
                return NotFound("Sipariş bulunamadı!");
            }
            
            // Durumu güncelle
            _orderService.TUpdateOrderStatus(orderId, status);
            
            // SignalR ile tüm bağlı kullanıcılara bildir
            await _hubContext.Clients.All.SendAsync("ReceiveOrderStatus", order.TableNumber, status);
            
            return Ok("Sipariş durumu güncellendi");
        }
    }
}
