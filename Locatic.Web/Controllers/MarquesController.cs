using Microsoft.AspNetCore.Mvc;
using Locatic.Web.Models;
using Locatic.Web.Services;

namespace Locatic.Web.Controllers;

public class MarquesController : Controller
{
    private readonly IMarqueService _marqueService;

    public MarquesController(IMarqueService marqueService)
    {
        _marqueService = marqueService;
    }

    public async Task<IActionResult> Index()
    {
        var marques = await _marqueService.GetAllAsync();
        return View(marques);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Marque marque)
    {
        if (!ModelState.IsValid)
        {
            return View(marque);
        }

        await _marqueService.CreateAsync(marque);
        return RedirectToAction("Index");
    }
}
