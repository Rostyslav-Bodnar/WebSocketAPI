namespace WebSocket_API.Entities
{
    public class PriceInfo
    {
        public int Id { get; set; }
        public int AssetId { get; set; }
        public Asset Asset { get; set; }
        public decimal Price { get; set; }
        public DateTime UpdatedAt { get; set; }

        public int Volume { get; set; }
        public decimal Change { get; set; }
        public decimal ChangePct { get; set; }
    }
}
