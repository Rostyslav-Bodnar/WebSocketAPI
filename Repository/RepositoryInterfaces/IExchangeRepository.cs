using WebSocket_API.Entities;

namespace WebSocket_API.Repository.RepositoryInterfaces
{
    public interface IExchangeRepository : IRepository<Exchange>
    {
        Task<Exchange> GetByNameAsync(string name);
    }
}
