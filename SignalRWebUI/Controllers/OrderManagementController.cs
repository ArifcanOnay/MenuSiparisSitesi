using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignalRWebUI.Dtos.OrderDtos;
using System.Text;

namespace SignalRWebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrderManagementController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public OrderManagementController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7186/api/Orders");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultOrderDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        public async Task<IActionResult> UpdateOrderStatus(int orderId, string status)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(status);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync($"https://localhost:7186/api/Orders/UpdateOrderStatus/{orderId}", stringContent);
            
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> SetOrderPreparing(int orderId, string tableNumber)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject("Sipariş Hazırlanıyor");
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync($"https://localhost:7186/api/Orders/UpdateOrderStatus/{orderId}", stringContent);
            
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = $"{tableNumber} - Sipariş hazırlanıyor olarak işaretlendi.";
            }
            
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> SetOrderCompleted(int orderId, string tableNumber)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject("Sipariş Tamamlandı");
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync($"https://localhost:7186/api/Orders/UpdateOrderStatus/{orderId}", stringContent);
            
            if (responseMessage.IsSuccessStatusCode)
            {
                // Masa numarasından ID'yi çıkar (örn: "Masa 1" -> 1)
                var tableId = tableNumber.Replace("Masa ", "").Trim();
                
                // Masayı boşa çek
                await client.GetAsync($"https://localhost:7186/api/MenuTables/ChangeMenuTableStatusToFalse?id={tableId}");
                
                TempData["SuccessMessage"] = $"{tableNumber} - Sipariş tamamlandı olarak işaretlendi ve masa boşa alındı.";
            }
            
            return RedirectToAction("Index");
        }
    }
}
