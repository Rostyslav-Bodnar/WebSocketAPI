using WebSocket_API.Entities;

namespace WebSocket_API.Repository.RepositoryInterfaces
{
    public interface IKindRepository : IRepository<Kind>
    {
        Task<Kind> GetByNameAsync(string name);
    }
}
