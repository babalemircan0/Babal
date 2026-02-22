using Microsoft.EntityFrameworkCore;
using Babal.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Veritabanı Ayarı: SQLite dosya yolunu belirliyoruz
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=BabalDb.db"));

builder.Services.AddControllersWithViews();

// 2. Session (Oturum) Ayarları
builder.Services.AddSession(options =>
    {
        options.IdleTimeout = TimeSpan.FromMinutes(30);
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
    });

// 3. Render/Vercel gibi platformlar için Port Ayarı (ÇOK ÖNEMLİ)
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

var app = builder.Build();

// 4. Veritabanını Uygulama Başlarken Otomatik Oluştur
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

// 5. Başlangıç Sayfası: Register (Kayıt)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Register}/{id?}");

app.Run();
