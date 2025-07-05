
using WebSocket_API.Entities;

namespace WebSocket_API.DTO
{
    public class InstrumentDTO
    {
        public string? Id { get; set; }
        public string? Symbol { get; set; }
        public string? Kind { get; set; }
        public string? Exchange { get; set; }
        public string? Description { get; set; }
        public double? TickSize { get; set; }
        public string? Currency { get; set; }
        public Dictionary<string, InstrumentMappingDTO> Mappings { get; set; }
        public InstrumentProfileDTO? Profile { get; set; }
    }
}
