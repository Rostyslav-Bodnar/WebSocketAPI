
using WebSocket_API.Mappers;
using WebSocket_API.Repository;
using WebSocket_API.Repository.RepositoryInterfaces;
using WebSocket_API.Services;

namespace WebSocket_API.Seeders
{
    public class KindDataSeeder : IDataSeeder
    {
        private readonly FintachartsKindService service;
        private readonly IKindRepository repository;

        public KindDataSeeder(FintachartsKindService service, IKindRepository repository)
        {
            this.service = service;
            this.repository = repository;
        }

        public async Task Seed()
        {
            var existingKinds = await repository.GetAllAsync();
            if (existingKinds.Any()) return;

            var dtos = await service.GetKinds();

            foreach (var dto in dtos)
            {
                var entity = KindMapper.ToEntity(dto);
                await repository.AddAsync(entity);
            }
        }
    }
}
