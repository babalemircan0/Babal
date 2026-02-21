using Microsoft.AspNetCore.Mvc;
using Babal.Data;
using Babal.Models;
using Microsoft.EntityFrameworkCore;

namespace Babal.Controllers
{
    public class AgendaController : Controller
    {
        private readonly AppDbContext _context;

        public AgendaController(AppDbContext context)
        {
            _context = context;
        }

        // Sayfayı açar: /Agenda/Index
        public IActionResult Index()
        {
            return View();
        }

        // Tüm ajanda verilerini JSON formatında getirir
        [HttpGet]
        public async Task<IActionResult> GetAgendaData()
        {
            // Veri tabanında kayıtlı tüm hücreleri (CellId anahtar olacak şekilde) çekiyoruz
            var data = await _context.Agendas
                .Where(a => a.CellId != null)
                .Select(a => new { a.CellId, a.TaskDescription })
                .ToDictionaryAsync(a => a.CellId!, a => a.TaskDescription ?? "");

            return Json(data);
        }

        // Görevi kaydeder, günceller veya siler
        [HttpPost]
        public async Task<IActionResult> UpdateTask(string cellId, string task)
        {
            if (string.IsNullOrEmpty(cellId)) return BadRequest("Geçersiz hücre ID.");

            // Veri tabanında bu hücreye (cell-0-8 vb.) ait kayıt var mı kontrol et
            var existing = await _context.Agendas.FirstOrDefaultAsync(a => a.CellId == cellId);

            if (string.IsNullOrWhiteSpace(task))
            {
                // Eğer kullanıcı metni sildiyse veya boşluk gönderdiyse, kaydı veri tabanından tamamen kaldır
                if (existing != null)
                {
                    _context.Agendas.Remove(existing);
                }
            }
            else
            {
                if (existing != null)
                {
                    // Daha önce kayıt varsa: Sadece içeriği güncelle
                    existing.TaskDescription = task;
                }
                else
                {
                    // İlk defa kayıt yapılıyorsa: Yeni bir 'Agenda' nesnesi oluştur ve ekle
                    _context.Agendas.Add(new Agenda
                    {
                        CellId = cellId,
                        TaskDescription = task
                    });
                }
            }

            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}