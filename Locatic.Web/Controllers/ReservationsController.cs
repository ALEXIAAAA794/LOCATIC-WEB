using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Locatic.Web.Data;
using Locatic.Web.Models;
using Locatic.Web.Services;
using Locatic.Web.ViewModels;

namespace Locatic.Web.Controllers;

public class ReservationsController : Controller
{
    private readonly IReservationService _reservationService;
    private readonly AppDbContext _db;

    public ReservationsController(
        IReservationService reservationService,
        AppDbContext db)
    {
        _reservationService = reservationService;
        _db = db;
    }

    // GET: Reservations
    public async Task<IActionResult> Index()
    {
        var reservations = await _reservationService.GetAllAsync();
        return View(reservations);
    }

    // GET: Reservations/Create
    public async Task<IActionResult> Create()
    {
        var vm = new ReservationCreateViewModel
        {
            Clients = new SelectList(
                await _db.Clients.ToListAsync(),
                "Id",
                "Nom"),

            Voitures = new SelectList(
                await _db.Voitures
                    .Include(v => v.Modele)
                    .ToListAsync(),
                "Id",
                "Immatriculation")
        };

        return View(vm);
    }

    // POST: Reservations/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        ReservationCreateViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            vm.Clients = new SelectList(
                await _db.Clients.ToListAsync(),
                "Id",
                "Nom");

            vm.Voitures = new SelectList(
                await _db.Voitures
                    .Include(v => v.Modele)
                    .ToListAsync(),
                "Id",
                "Immatriculation");

            return View(vm);
        }

        var reservation = new Reservation
        {
            ClientId = vm.ClientId,
            VoitureId = vm.VoitureId,
            DateDebut = vm.DateDebut,
            DateFin = vm.DateFin
        };

        var result = await _reservationService.CreateAsync(reservation);

        if (!result.success)
        {
            ModelState.AddModelError(
                "",
                result.error ?? "Erreur lors de la création de la réservation.");

            vm.Clients = new SelectList(
                await _db.Clients.ToListAsync(),
                "Id",
                "Nom");

            vm.Voitures = new SelectList(
                await _db.Voitures
                    .Include(v => v.Modele)
                    .ToListAsync(),
                "Id",
                "Immatriculation");

            return View(vm);
        }

        return RedirectToAction(nameof(Index));
    }

    // GET: Reservations/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var reservation = await _reservationService.GetByIdAsync(id);

        if (reservation == null)
            return NotFound();

        return View(reservation);
    }

    // POST: Reservations/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _reservationService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}