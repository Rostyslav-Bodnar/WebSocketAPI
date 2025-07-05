using WebSocket_API.Entities;
using WebSocket_API.Mappers;
using WebSocket_API.Repository;
using WebSocket_API.Repository.RepositoryInterfaces;
using WebSocket_API.Services;

namespace WebSocket_API.Seeders
{
    public class InstrumentDataSeeder : IDataSeeder
    {
        private readonly FintachartsInstrumentService service;
        private readonly IInstrumentRepository repository;

        private readonly IKindRepository kindRepository;
        private readonly IExchangeRepository exchangeRepository;
        private readonly IInstrumentProfileRepository instrumentProfileRepository;
        private readonly IProviderRepository providerRepository;

        public InstrumentDataSeeder(FintachartsInstrumentService service,
            IInstrumentRepository repository, IKindRepository kindRepository, 
            IExchangeRepository exchangeRepository, 
            IInstrumentProfileRepository instrumentProfileRepository, IProviderRepository providerRepository)
        {
            this.service = service;
            this.repository = repository;
            this.kindRepository = kindRepository;
            this.exchangeRepository = exchangeRepository;
            this.instrumentProfileRepository = instrumentProfileRepository;
            this.providerRepository = providerRepository;
        }

        public async Task Seed()
        {
            var existingInstruments = await repository.GetAllAsync();
            if (existingInstruments.Any()) return;

            var dtos = await service.GetInstrumentsAsync();

            foreach (var dto in dtos)
            {
                var kind = await kindRepository.GetByNameAsync(dto.Kind);
                var exchange = await exchangeRepository.GetByNameAsync(dto.Exchange);

                var mappings = new List<InstrumentMapping>();
                if (dto.Mappings != null)
                {
                    foreach (var map in dto.Mappings)
                    {
                        var providerName = map.Key;
                        var mappingDto = map.Value;
                        var provider = await providerRepository.GetByName(providerName);
                        var mappingExchange = await exchangeRepository.GetByNameAsync(mappingDto.Exchange);

                        var mappingEntity = InstrumentMappingMapper.ToEntity(mappingDto, provider, mappingExchange);

                        if (provider != null)
                            mappingEntity.Provider = provider;
                        else
                            throw new KeyNotFoundException("Provider not found");

                        mappings.Add(mappingEntity);
                    }
                }

                var entity = InstrumentMapper.ToEntity(dto, kind, exchange, mappings);
                await repository.AddAsync(entity);
            }
        }

    }
}
