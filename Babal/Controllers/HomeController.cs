using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Babal.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            return View();
        }
    }
}   