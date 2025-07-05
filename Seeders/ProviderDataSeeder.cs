using WebSocket_API.Mappers;
using WebSocket_API.Repository.RepositoryInterfaces;
using WebSocket_API.Services;

namespace WebSocket_API.Seeders
{
    public class ProviderDataSeeder : IDataSeeder
    {
        private readonly FintachartsProviderService service;
        private readonly IProviderRepository repository;

        public ProviderDataSeeder(FintachartsProviderService service, IProviderRepository repository)
        {
            this.service = service;
            this.repository = repository;
        }

        public async Task Seed()
        {
            var existingProviders = await repository.GetAllAsync();
            if (existingProviders.Any()) return;

            var dtos = await service.GetProvidersAsync();

            foreach (var dto in dtos)
            {
                var entity = ProviderMapper.ToEntity(dto);
                await repository.AddAsync(entity);
            }

        }
    }
}
