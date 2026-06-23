using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Locatic.Web.Models;
using System.IO;

namespace Locatic.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    // Serve the uploaded home background image from the project root
    [ResponseCache(Duration = 86400)]
    public IActionResult BackgroundImage()
    {
        var fileName = "73-entrepot-avec-voitures-en-hauteur-sur-rayonnages.jpg";
        var path = Path.Combine(Directory.GetCurrentDirectory(), fileName);
        if (!System.IO.File.Exists(path))
            return NotFound();

        return PhysicalFile(path, "image/jpeg");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
