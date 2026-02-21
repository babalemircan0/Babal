using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Babal.Models;
using Babal.Data;
using System.Linq;

namespace Babal.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        public AccountController(AppDbContext context) { _context = context; }

        // KAYIT SAYFASI (Açılış Sayfası)
        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                // Kayıt bitince Giriş sayfasına gönder
                return RedirectToAction("Login");
            }
            return View(user);
        }

        // GİRİŞ SAYFASI
        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(string Email, string Password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == Email && u.Password == Password);
            if (user != null)
            {
                HttpContext.Session.SetString("UserName", user.FullName);
                HttpContext.Session.SetInt32("UserId", user.Id);
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Error = "Hatalı giriş!";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}