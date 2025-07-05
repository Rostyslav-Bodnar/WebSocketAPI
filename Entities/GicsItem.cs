
namespace WebSocket_API.Entities
{
    public class GicsItem
    {
        public int Id { get; set; }
        public int GicsId { get; set; }
        public int? GicsParentId { get; set; }
        public int? ParentId { get; set; }

        public string Name { get; set; } = string.Empty;

        public GicsItem? Parent { get; set; }
        public List<GicsItem>? Children { get; set; }
    }
}
