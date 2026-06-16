using Microsoft.EntityFrameworkCore;
using Locatic.Web.Data;
using Locatic.Web.Repositories;
using Locatic.Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// SQLite via EF Core
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=locatic.db"));

// Injection des repositories
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Injection des services
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IReservationService, ReservationService>();

// TODO Membre A : builder.Services.AddScoped<IVoitureService, VoitureService>();
// TODO Membre A : builder.Services.AddScoped<IMarqueService, MarqueService>();
// TODO Membre A : builder.Services.AddScoped<IModeleService, ModeleService>();

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

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();