using WebSocket_API.Entities;

namespace WebSocket_API.Repository.RepositoryInterfaces
{
    public interface IInstrumentRepository : IRepository<Instrument>
    {
        Task<Instrument?> GetByInstrumentId(string instrumentId);
        Task<Instrument?> GetBySymbol(string symbol);
    }
}
