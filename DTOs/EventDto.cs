namespace EventBookingSystem.DTOs
{
    public class EventDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public DateTime Date { get; set; }
        public string? Venue { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
    }
}
