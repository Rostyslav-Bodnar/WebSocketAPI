using WebSocket_API.DTO;
using WebSocket_API.Entities;

namespace WebSocket_API.Mappers
{
    public static class InstrumentProfileMapper
    {
        public static InstrumentProfileDTO ToDto(InstrumentProfile profile) => new()
        {
            Name = profile.Name,
            Location = profile.Location,
            Gics = GicsClassificationMapper.ToDto(profile.GicsClassification)
        };

        public static InstrumentProfile ToEntity(InstrumentProfileDTO dto) => new()
        {
            Name = dto.Name ?? string.Empty,
            Location = dto.Location ?? string.Empty,
            GicsClassification = GicsClassificationMapper.ToEntity(dto.Gics)
        };
    }
}
