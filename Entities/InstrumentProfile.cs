namespace WebSocket_API.Entities
{
    public class InstrumentProfile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int GicsClassificationId { get; set; }
        public GicsClassification GicsClassification { get; set; }
    }
}
