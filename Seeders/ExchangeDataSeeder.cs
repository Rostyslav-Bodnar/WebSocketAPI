using WebSocket_API.Mappers;
using WebSocket_API.Repository.RepositoryInterfaces;
using WebSocket_API.Services;

namespace WebSocket_API.Seeders
{
    public class ExchangeDataSeeder : IDataSeeder
    {
        private readonly FintachartsExchangeService service;
        private readonly IExchangeRepository repository;

        public ExchangeDataSeeder(FintachartsExchangeService service, IExchangeRepository repository)
        {
            this.service = service;
            this.repository = repository;
        }

        public async Task Seed()
        {
            var existingExchanges = await repository.GetAllAsync();
            if (existingExchanges.Any()) return;

            var dtos = await service.GetExchangeAsync();

            foreach (var dto in dtos)
            {
                var entity = ExchangeMapper.ToEntity(dto);
                await repository.AddAsync(entity);
            }
        }
    }
}
