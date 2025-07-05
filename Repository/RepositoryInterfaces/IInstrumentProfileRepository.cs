using WebSocket_API.Entities;

namespace WebSocket_API.Repository.RepositoryInterfaces
{
    public interface IInstrumentProfileRepository : IRepository<InstrumentProfile>
    {
        Task<InstrumentProfile> GetByInstrumentIdAsync(string instrumentId);
    }
}
