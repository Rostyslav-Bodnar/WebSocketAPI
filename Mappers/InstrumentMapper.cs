using WebSocket_API.DTO;
using WebSocket_API.Entities;

namespace WebSocket_API.Mappers
{
    public static class InstrumentMapper
    {
        public static InstrumentDTO ToDto(Instrument instrument) => new()
        {
            Id = instrument.InstrumentId,
            Symbol = instrument.Symbol,
            Kind = instrument.Kind != null ? instrument.Kind.Name : null,
            Exchange = instrument.Exchange?.Name,
            Description = instrument.Description,
            TickSize = instrument.tickSize,
            Currency = instrument.Currency,
            Profile = instrument.InstrumentProfile != null ? InstrumentProfileMapper.ToDto(instrument.InstrumentProfile) : null,
            Mappings = instrument.InstrumentMappings?.ToDictionary(
                m => m.Provider?.Name ?? "",
                m => InstrumentMappingMapper.ToDto(m)
            ) ?? new Dictionary<string, InstrumentMappingDTO>()
        };

        public static Instrument ToEntity(InstrumentDTO dto, Kind kind, Exchange exchange, List<InstrumentMapping> instrumentMappings)
        {
            var instrument = new Instrument
            {
                InstrumentId = dto.Id ?? string.Empty,
                Symbol = dto.Symbol,
                Kind = kind,
                Exchange = exchange,
                Description = dto.Description,
                tickSize = dto.TickSize,
                Currency = dto.Currency,
                InstrumentProfile = InstrumentProfileMapper.ToEntity(dto.Profile),
                InstrumentMappings = instrumentMappings
            };

            return instrument;
        }
    }
}
