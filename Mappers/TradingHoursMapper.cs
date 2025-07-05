using WebSocket_API.DTO;
using WebSocket_API.Entities;

namespace WebSocket_API.Mappers
{
    public static class TradingHoursMapper
    {
        public static TradingHoursDTO ToDto(TradingHours th) => new()
        {
            RegularStart = th.RegularStart,
            RegularEnd = th.RegularEnd,
            ElectronicStart = th.ElectronicStart,
            ElectronicEnd = th.ElectronicEnd
        };

        public static TradingHours ToEntity(TradingHoursDTO dto) => new()
        {
            RegularStart = dto.RegularStart,
            RegularEnd = dto.RegularEnd,
            ElectronicStart = dto.ElectronicStart,
            ElectronicEnd = dto.ElectronicEnd
        };
    }
}
