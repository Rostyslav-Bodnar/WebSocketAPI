﻿namespace WebSocket_API.DTO
{
    public class SupportedAssetsDTO
    {
        public string? InstrumentId { get; set; }
        public string? Provider { get; set; }
        public string? Symbol { get; set; }
        public decimal? Price { get; set; }
    }
}
