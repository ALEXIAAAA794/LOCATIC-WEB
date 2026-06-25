using Locatic.Web.Models;
using Locatic.Web.Repositories;

namespace Locatic.Web.Services;

public class ClientService : IClientService
{
    private readonly IRepository<Client> _repository;

    public ClientService(IRepository<Client> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Client>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Client?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task CreateAsync(Client client)
    {
        await _repository.AddAsync(client);
    }

    public async Task UpdateAsync(Client client)
    {
        await _repository.UpdateAsync(client);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}