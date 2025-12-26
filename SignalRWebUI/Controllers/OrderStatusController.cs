using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignalRWebUI.Dtos.OrderDtos;

namespace SignalRWebUI.Controllers
{
    [AllowAnonymous]
    public class OrderStatusController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public OrderStatusController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index(int menuTableId)
        {
            ViewBag.MenuTableId = menuTableId;
            ViewBag.TableNumber = "Masa " + menuTableId;
            
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7186/api/Orders/GetLastOrderByTableNumber?tableNumber=Masa {menuTableId}");
            
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var order = JsonConvert.DeserializeObject<ResultOrderDto>(jsonData);
                return View(order);
            }
            
            return View(null);
        }
    }
}
