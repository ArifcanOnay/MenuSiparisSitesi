using Microsoft.AspNetCore.Mvc;
using QRCoder;

namespace SignalRWebUI.Controllers
{
    public class QRCodeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string value)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(value, QRCodeGenerator.ECCLevel.Q);
            PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
            byte[] qrCodeAsPngByteArr = qrCode.GetGraphic(10);
            string qrCodeBase64 = Convert.ToBase64String(qrCodeAsPngByteArr);
            
            ViewBag.QrCodeImage = "data:image/png;base64," + qrCodeBase64;
            
            return View();
        }
    }
}
