using WebSocket_API.Entities;

namespace WebSocket_API.DTO
{
    public class InstrumentProfileDTO
    {
        public string? Name { get; set; }
        public string? Location { get; set; }
        public GicsClassificationDTO Gics { get; set; }

    }
}
