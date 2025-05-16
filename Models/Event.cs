using System.ComponentModel.DataAnnotations.Schema;

namespace EventBookingSystem.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public DateTime Date { get; set; }
        public string? Venue { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public List<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
