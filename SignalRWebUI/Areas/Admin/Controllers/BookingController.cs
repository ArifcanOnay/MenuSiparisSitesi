using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignalRWebUI.Dtos.BookingDtos;
using SignalR.BusinessLayer.Abstract;
using System.Text;

namespace SignalRWebUI.Areas.Admin.Controllers
{
    [Area("Admin")][Authorize(Roles = "Admin")]
    public class BookingController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IEmailService _emailService;
        
        public BookingController(IHttpClientFactory httpClientFactory, IEmailService emailService)
        {
            _httpClientFactory = httpClientFactory;
            _emailService = emailService;
        }
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7186/api/Booking");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultBookingDto>>(jsonData);
                return View(values);
            }
            return View();
        }
        [HttpGet]
        public IActionResult CreateBooking()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateBooking(CreateBookingDto createBookingDto)
        {
            createBookingDto.Description = "Rezervasyon Alýndý";
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createBookingDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7186/api/Booking", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7186/api/Booking/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> UpdateBooking(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7186/api/Booking/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateBookingDto>(jsonData);
                return View(values);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateBooking(UpdateBookingDto updateBookingDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateBookingDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7186/api/Booking/", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<IActionResult> BookingStatusApproved(int id)
        {
            var client = _httpClientFactory.CreateClient();
            
            // Rezervasyon bilgilerini al
            var getResponse = await client.GetAsync($"https://localhost:7186/api/Booking/{id}");
            if (getResponse.IsSuccessStatusCode)
            {
                var jsonData = await getResponse.Content.ReadAsStringAsync();
                var booking = JsonConvert.DeserializeObject<ResultBookingDto>(jsonData);
                
                // Durumu güncelle
                await client.GetAsync($"https://localhost:7186/api/Booking/BookingStatusApproved/{id}");
                
                // Mail gönder
                var subject = "?? Rezervasyonunuz Onaylandý!";
                var body = $@"
                    <html>
                    <body style='font-family: Arial, sans-serif;'>
                        <div style='max-width: 600px; margin: 0 auto; padding: 20px; background-color: #f8f9fa;'>
                            <div style='background-color: #28a745; color: white; padding: 20px; text-align: center; border-radius: 10px 10px 0 0;'>
                                <h1>? Rezervasyonunuz Onaylandý!</h1>
                            </div>
                            <div style='background-color: white; padding: 30px; border-radius: 0 0 10px 10px;'>
                                <p>Sayýn <strong>{booking.Name}</strong>,</p>
                                <p>Rezervasyonunuz baþarýyla onaylanmýþtýr.</p>
                                
                                <div style='background-color: #e9ecef; padding: 15px; border-radius: 5px; margin: 20px 0;'>
                                    <h3 style='color: #28a745; margin-top: 0;'>Rezervasyon Detaylarý:</h3>
                                    <p><strong>Ad Soyad:</strong> {booking.Name}</p>
                                    <p><strong>Telefon:</strong> {booking.Phone}</p>
                                    <p><strong>Kiþi Sayýsý:</strong> {booking.PersonCount}</p>
                                    <p><strong>Tarih:</strong> {booking.Date:dd.MM.yyyy HH:mm}</p>
                                </div>
                                
                                <p>Sizi aramýzda görmekten mutluluk duyacaðýz!</p>
                                <p style='color: #6c757d; font-size: 12px; margin-top: 30px;'>
                                    Herhangi bir sorunuz varsa bizimle iletiþime geçebilirsiniz.
                                </p>
                            </div>
                        </div>
                    </body>
                    </html>";
                
                await _emailService.SendEmailAsync(booking.Mail, subject, body);
            }
            
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> BookingStatusCancelled(int id)
        {
            var client = _httpClientFactory.CreateClient();
            
            // Rezervasyon bilgilerini al
            var getResponse = await client.GetAsync($"https://localhost:7186/api/Booking/{id}");
            if (getResponse.IsSuccessStatusCode)
            {
                var jsonData = await getResponse.Content.ReadAsStringAsync();
                var booking = JsonConvert.DeserializeObject<ResultBookingDto>(jsonData);
                
                // Durumu güncelle
                await client.GetAsync($"https://localhost:7186/api/Booking/BookingStatusCancelled/{id}");
                
                // Mail gönder
                var subject = "? Rezervasyonunuz Ýptal Edildi";
                var body = $@"
                    <html>
                    <body style='font-family: Arial, sans-serif;'>
                        <div style='max-width: 600px; margin: 0 auto; padding: 20px; background-color: #f8f9fa;'>
                            <div style='background-color: #dc3545; color: white; padding: 20px; text-align: center; border-radius: 10px 10px 0 0;'>
                                <h1>? Rezervasyon Ýptali</h1>
                            </div>
                            <div style='background-color: white; padding: 30px; border-radius: 0 0 10px 10px;'>
                                <p>Sayýn <strong>{booking.Name}</strong>,</p>
                                <p>Maalesef rezervasyonunuz iptal edilmiþtir.</p>
                                
                                <div style='background-color: #f8d7da; padding: 15px; border-radius: 5px; margin: 20px 0; border-left: 4px solid #dc3545;'>
                                    <h3 style='color: #dc3545; margin-top: 0;'>Ýptal Edilen Rezervasyon:</h3>
                                    <p><strong>Ad Soyad:</strong> {booking.Name}</p>
                                    <p><strong>Telefon:</strong> {booking.Phone}</p>
                                    <p><strong>Kiþi Sayýsý:</strong> {booking.PersonCount}</p>
                                    <p><strong>Tarih:</strong> {booking.Date:dd.MM.yyyy HH:mm}</p>
                                </div>
                                
                                <p>Yeni bir rezervasyon oluþturmak için web sitemizi ziyaret edebilirsiniz.</p>
                                <p style='color: #6c757d; font-size: 12px; margin-top: 30px;'>
                                    Sorularýnýz için bizimle iletiþime geçebilirsiniz.
                                </p>
                            </div>
                        </div>
                    </body>
                    </html>";
                
                await _emailService.SendEmailAsync(booking.Mail, subject, body);
            }
            
            return RedirectToAction("Index");
        }
	}
}
