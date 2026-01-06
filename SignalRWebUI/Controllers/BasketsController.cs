using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignalRWebUI.Dtos.BasketDtos;
using System.Text;

namespace SignalRWebUI.Controllers
{
    [AllowAnonymous]
    public class BasketsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public BasketsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.id = id;
            TempData["id"] = id;
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7186/api/Basket/BasketListByMenuTableWithProductName?id=" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultBasketDto>>(jsonData);
                return View(values);
            }
            return View();
        }
        public async Task<IActionResult> DeleteBasket(int id)
        {
            if (TempData["id"] == null)
            {
                return RedirectToAction("Index", new { id = 1 });
            }
            
            var menuTableId = int.Parse(TempData["id"]?.ToString() ?? "1");
            TempData.Keep("id"); // TempData'yı koru
            
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7186/api/Basket/{id}");
            
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Ürün sepetten silindi!";
                return RedirectToAction("Index", new { id = menuTableId });
            }
            return NoContent();
        }

        public async Task<IActionResult> CompleteOrder(int menuTableId)
        {
            var client = _httpClientFactory.CreateClient();
            
            // Sipariş oluştur
            var responseMessage = await client.PostAsync($"https://localhost:7186/api/Orders/CreateOrderFromBasket?menuTableId={menuTableId}", null);
            
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "✅ Siparişiniz başarıyla alındı! Mutfağa iletildi.";
                
                // Menu sayfasına geri dön
                return RedirectToAction("Index", "Menu", new { id = menuTableId });
            }
            
            TempData["ErrorMessage"] = "❌ Sipariş oluşturulurken bir hata oluştu!";
            return RedirectToAction("Index", new { id = menuTableId });
        }
    }
}
