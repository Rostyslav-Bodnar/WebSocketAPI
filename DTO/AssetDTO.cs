namespace WebSocket_API.DTO
{
    public class AssetDTO
    {
        public string? InstrumentId { get; set; }
        public string? Provider { get; set; }
        public string? Symbol { get; set; }
        public decimal? Price { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public decimal? Change { get; set; }

    }
}
