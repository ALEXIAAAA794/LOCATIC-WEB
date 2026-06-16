using Microsoft.EntityFrameworkCore;
using Locatic.Web.Data;
using Locatic.Web.Repositories;
using Locatic.Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// SQLite via EF Core
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=locatic.db"));

// Injection des services et du repository générique
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IVoitureService, VoitureService>();
builder.Services.AddScoped<IMarqueService, MarqueService>();
builder.Services.AddScoped<IModeleService, ModeleService>();
// TODO Membre B : builder.Services.AddScoped<IClientService, ClientService>();

var app = builder.Build();

// Appliquer les migrations et le seed au démarrage
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

if (!app.Environment.IsDevelopment())
    app.UseExceptionHandler("/Home/Error");

app.UseStaticFiles();
app.UseRouting();
app.MapDefaultControllerRoute();

app.Run();
