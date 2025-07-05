using WebSocket_API.Entities;

namespace WebSocket_API.Repository.RepositoryInterfaces
{
    public interface IProviderRepository : IRepository<Provider>
    {
        Task<Provider?> GetByName(string name);
    }
}
