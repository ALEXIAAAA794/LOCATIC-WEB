using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Locatic.Web.Models;
using Locatic.Web.Services;
using Locatic.Web.ViewModels;

namespace Locatic.Web.Controllers;

public class VoituresController : Controller
{
    private readonly IVoitureService _voitureService;
    private readonly IModeleService _modeleService;

    public VoituresController(IVoitureService voitureService, IModeleService modeleService)
    {
        _voitureService = voitureService;
        _modeleService = modeleService;
    }

    public async Task<IActionResult> Index()
    {
        var voitures = await _voitureService.GetAllAsync();
        return View(voitures);
    }

    public async Task<IActionResult> Details(int id)
    {
        var voiture = await _voitureService.GetByIdAsync(id);
        if (voiture == null)
        {
            return NotFound();
        }

        return View(voiture);
    }

    public async Task<IActionResult> Create()
    {
        var modeles = await _modeleService.GetAllAsync();
        var vm = new VoitureFormViewModel();
        vm.Modeles = new SelectList(modeles, "Id", "Nom");
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(VoitureFormViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            var modeles = await _modeleService.GetAllAsync();
            vm.Modeles = new SelectList(modeles, "Id", "Nom");
            return View(vm);
        }

        var voiture = new Voiture();
        voiture.Immatriculation = vm.Immatriculation;
        voiture.Annee = vm.Annee;
        voiture.TarifJournalier = vm.TarifJournalier;
        voiture.NombrePlaces = vm.NombrePlaces;
        voiture.Carburant = vm.Carburant;
        voiture.ModeleId = vm.ModeleId;

        await _voitureService.CreateAsync(voiture);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Edit(int id)
    {
        var voiture = await _voitureService.GetByIdAsync(id);
        if (voiture == null)
        {
            return NotFound();
        }

        var modeles = await _modeleService.GetAllAsync();

        var vm = new VoitureFormViewModel();
        vm.Id = voiture.Id;
        vm.Immatriculation = voiture.Immatriculation;
        vm.Annee = voiture.Annee;
        vm.TarifJournalier = voiture.TarifJournalier;
        vm.NombrePlaces = voiture.NombrePlaces;
        vm.Carburant = voiture.Carburant;
        vm.ModeleId = voiture.ModeleId;
        vm.Modeles = new SelectList(modeles, "Id", "Nom", voiture.ModeleId);

        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(VoitureFormViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            var modeles = await _modeleService.GetAllAsync();
            vm.Modeles = new SelectList(modeles, "Id", "Nom");
            return View(vm);
        }

        var voiture = new Voiture();
        voiture.Id = vm.Id;
        voiture.Immatriculation = vm.Immatriculation;
        voiture.Annee = vm.Annee;
        voiture.TarifJournalier = vm.TarifJournalier;
        voiture.NombrePlaces = vm.NombrePlaces;
        voiture.Carburant = vm.Carburant;
        voiture.ModeleId = vm.ModeleId;

        await _voitureService.UpdateAsync(voiture);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int id)
    {
        var voiture = await _voitureService.GetByIdAsync(id);
        if (voiture == null)
        {
            return NotFound();
        }

        return View(voiture);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _voitureService.DeleteAsync(id);
        return RedirectToAction("Index");
    }
}
