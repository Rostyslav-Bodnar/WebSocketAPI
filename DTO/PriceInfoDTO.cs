namespace WebSocket_API.DTO
{
    public class PriceInfoDTO
    {
        public decimal Price { get; set; }
        public DateTime UpdatedAt { get; set; }

        public int Volume { get; set; }
        public decimal Change { get; set; }
        public decimal ChangePct { get; set; }
    }
}
