using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SignalR.EntityLayer.Entities;
using SignalRWebUI.Dtos.IdentityDtos;

namespace SignalRWebUI.Controllers
{
    [Route("[controller]")]
    public class SettingController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public SettingController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        // Admin için (mevcut)
        [HttpGet]
        [Route("Index")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            UserEditDto userEditDto = new UserEditDto();
            userEditDto.Surname = values.Surname;
            userEditDto.Name = values.Name;
            userEditDto.Username = values.UserName;
            userEditDto.Mail = values.Email;
            return View(userEditDto);
        }
        
        [HttpPost]
        [Route("Index")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index(UserEditDto userEditDto)
        {
            if (userEditDto.Password == userEditDto.ConfirmPassword)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                user.Name=userEditDto.Name;
                user.Surname=userEditDto.Surname;
                user.Email = userEditDto.Mail;
                user.UserName = userEditDto.Username;
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, userEditDto.Password);
                await _userManager.UpdateAsync(user);
                return RedirectToAction("Index", "Category");
            }
            return View();
        }

        // Kullanıcılar için (YENİ)
        [HttpGet]
        [Route("UserProfile")]
        [Authorize]
        public async Task<IActionResult> UserProfile()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }

            UserEditDto userEditDto = new UserEditDto
            {
                Name = user.Name,
                Surname = user.Surname,
                Username = user.UserName,
                Mail = user.Email
            };
            
            return View(userEditDto);
        }

        [HttpPost]
        [Route("UserProfile")]
        [Authorize]
        public async Task<IActionResult> UserProfile(UserEditDto userEditDto)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }

            // Şifre değiştirme kontrolü
            if (!string.IsNullOrEmpty(userEditDto.Password))
            {
                if (userEditDto.Password != userEditDto.ConfirmPassword)
                {
                    TempData["ErrorMessage"] = "Şifreler eşleşmiyor!";
                    return View(userEditDto);
                }
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, userEditDto.Password);
            }

            // Bilgileri güncelle
            user.Name = userEditDto.Name;
            user.Surname = userEditDto.Surname;
            user.Email = userEditDto.Mail;
            
            var result = await _userManager.UpdateAsync(user);
            
            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Bilgileriniz başarıyla güncellendi!";
                return RedirectToAction("UserProfile");
            }
            else
            {
                TempData["ErrorMessage"] = "Güncelleme sırasında bir hata oluştu!";
            }

            return View(userEditDto);
        }
    }
}
