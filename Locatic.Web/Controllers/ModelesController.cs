using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Locatic.Web.Models;
using Locatic.Web.Services;

namespace Locatic.Web.Controllers;

public class ModelesController : Controller
{
    private readonly IModeleService _modeleService;
    private readonly IMarqueService _marqueService;

    public ModelesController(IModeleService modeleService, IMarqueService marqueService)
    {
        _modeleService = modeleService;
        _marqueService = marqueService;
    }

    public async Task<IActionResult> Index()
    {
        var modeles = await _modeleService.GetAllAsync();
        return View(modeles);
    }

    public async Task<IActionResult> Create()
    {
        var marques = await _marqueService.GetAllAsync();
        ViewData["Marques"] = new SelectList(marques, "Id", "Nom");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Modele modele)
    {
        if (!ModelState.IsValid)
        {
            var marques = await _marqueService.GetAllAsync();
            ViewData["Marques"] = new SelectList(marques, "Id", "Nom", modele.MarqueId);
            return View(modele);
        }

        await _modeleService.CreateAsync(modele);
        return RedirectToAction("Index");
    }
}
