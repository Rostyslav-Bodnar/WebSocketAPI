
using WebSocket_API.Mappers;
using WebSocket_API.Repository.RepositoryInterfaces;
using WebSocket_API.Services;

namespace WebSocket_API.Seeders
{
    public class GicsDataSeeder : IDataSeeder
    {
        private readonly FintachartsGicsService service;
        private readonly IGicsItemRepository repository;

        public GicsDataSeeder(FintachartsGicsService service, IGicsItemRepository repository)
        {
            this.service = service;
            this.repository = repository;
        }

        public async Task Seed()
        {
            var existingGics = await repository.GetAllAsync();
            if (existingGics.Any()) return;

            var dtos = await service.GetGicsItemsAsync();

            foreach (var dto in dtos)
            {
                var entity = GicsItemMapper.ToEntity(dto);
                await repository.AddAsync(entity);
            }
        }
    }
}
