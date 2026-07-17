using Microsoft.EntityFrameworkCore;
using Locatic.Web.Data;
using Locatic.Web.Repositories;
using Locatic.Web.Services;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Choix du chemin de la base SQLite selon l'environnement
var dbPath = builder.Environment.IsDevelopment()
    ? "Data Source=locatic.db"
    : "Data Source=/data/locatic.db";

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(dbPath));

// Injection des services et du repository générique
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IVoitureService, VoitureService>();
builder.Services.AddScoped<IMarqueService, MarqueService>();
builder.Services.AddScoped<IModeleService, ModeleService>();
builder.Services.AddScoped<IClientService, ClientService>();

var app = builder.Build();

// Appliquer les migrations au démarrage
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Active les métriques HTTP Prometheus
app.UseHttpMetrics();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Expose les métriques sur /metrics
app.MapMetrics();

app.MapGet("/ping", () => "OK");

app.Run();