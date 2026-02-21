using Microsoft.AspNetCore.Mvc;
using Babal.Data;
using Babal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http; // Session için gerekli

namespace Babal.Controllers
{
    public class GunlukController : Controller
    {
        private readonly AppDbContext _context;

        public GunlukController(AppDbContext context)
        {
            _context = context;
        }

        // Sayfa açıldığında giriş kontrolü yapıyoruz
        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetEntries()
        {
            // Hafızadaki kullanıcı ID'sini alıyoruz
            int? currentUserId = HttpContext.Session.GetInt32("UserId");
            if (currentUserId == null) return Unauthorized();

            // Sadece bu kullanıcıya (Emircan'a) ait notları getir
            var entries = await _context.DiaryEntries
                .Where(d => d.UserId == currentUserId) // FİLTRE: Kişiye özel notlar
                .OrderByDescending(d => d.CreatedDate)
                .Select(d => new {
                    id = d.Id,
                    title = d.Title,
                    content = d.Content,
                    date = d.CreatedDate.ToString("dd MMMM yyyy").ToUpper()
                })
                .ToListAsync();

            return Json(entries);
        }

        [HttpPost]
        public async Task<IActionResult> SaveEntry(int? id, string title, string content)
        {
            int? currentUserId = HttpContext.Session.GetInt32("UserId");
            if (currentUserId == null) return Unauthorized();

            if (string.IsNullOrEmpty(content)) return BadRequest();

            if (id.HasValue && id > 0)
            {
                // Düzenleme yaparken de başkasının notunu düzenlemediğinden emin oluyoruz
                var existing = await _context.DiaryEntries
                    .FirstOrDefaultAsync(d => d.Id == id.Value && d.UserId == currentUserId);

                if (existing != null)
                {
                    existing.Title = title ?? "Başlıksız";
                    existing.Content = content;
                }
            }
            else
            {
                // Yeni kayıt oluştururken kullanıcının ID'sini ekliyoruz
                var entry = new DiaryEntry
                {
                    Title = title ?? "Başlıksız",
                    Content = content,
                    CreatedDate = DateTime.Now,
                    UserId = currentUserId.Value // Kayıt sahibini Emircan yapıyoruz
                };
                _context.DiaryEntries.Add(entry);
            }

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEntry(int id)
        {
            int? currentUserId = HttpContext.Session.GetInt32("UserId");
            if (currentUserId == null) return Unauthorized();

            // Silme işleminde de güvenlik: Sadece kendi notunu silebilir
            var entry = await _context.DiaryEntries
                .FirstOrDefaultAsync(d => d.Id == id && d.UserId == currentUserId);

            if (entry != null)
            {
                _context.DiaryEntries.Remove(entry);
                await _context.SaveChangesAsync();
            }
            return Ok();
        }
    }
}