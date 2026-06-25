using Microsoft.AspNetCore.Mvc;
using Locatic.Web.Models;
using Locatic.Web.Services;

namespace Locatic.Web.Controllers;

public class ClientsController : Controller
{
    private readonly IClientService _clientService;

    public ClientsController(IClientService clientService)
    {
        _clientService = clientService;
    }

    // GET: Clients
    public async Task<IActionResult> Index()
    {
        var clients = await _clientService.GetAllAsync();
        return View(clients);
    }

    // GET: Clients/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var client = await _clientService.GetByIdAsync(id);

        if (client == null)
            return NotFound();

        return View(client);
    }

    // GET: Clients/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Clients/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Client client)
    {
        if (!ModelState.IsValid)
            return View(client);

        await _clientService.CreateAsync(client);

        return RedirectToAction(nameof(Index));
    }

    // GET: Clients/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var client = await _clientService.GetByIdAsync(id);

        if (client == null)
            return NotFound();

        return View(client);
    }

    // POST: Clients/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Client client)
    {
        if (id != client.Id)
            return NotFound();

        if (!ModelState.IsValid)
            return View(client);

        await _clientService.UpdateAsync(client);

        return RedirectToAction(nameof(Index));
    }

    // GET: Clients/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var client = await _clientService.GetByIdAsync(id);

        if (client == null)
            return NotFound();

        return View(client);
    }

    // POST: Clients/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _clientService.DeleteAsync(id);

        return RedirectToAction(nameof(Index));
    }
}