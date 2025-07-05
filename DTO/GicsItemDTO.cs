using WebSocket_API.Entities;

namespace WebSocket_API.DTO
{
    public class GicsItemDTO
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }

        public string Name { get; set; } = string.Empty;

        public List<GicsItemDTO>? Items { get; set; }
    }
}
