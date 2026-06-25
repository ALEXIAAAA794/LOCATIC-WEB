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
        return View(new Modele());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Modele modele)
    {
        // Fix : EF Core invalide la propriété de navigation Marque qui est null
        ModelState.Remove("Marque");

        if (!ModelState.IsValid)
        {
            var marques = await _marqueService.GetAllAsync();
            ViewData["Marques"] = new SelectList(marques, "Id", "Nom", modele.MarqueId);
            return View(modele);
        }

        await _modeleService.CreateAsync(modele);
        TempData["SuccessMessage"] = "Le modèle a été ajouté avec succès.";
        return RedirectToAction("Index");
    }
}