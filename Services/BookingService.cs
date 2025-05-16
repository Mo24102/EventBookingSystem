using EventBookingSystem.Data;
using EventBookingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace EventBookingSystem.Services
{
    public class BookingService
    {
        private readonly AppDbContext _context;

        public BookingService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateBookingAsync(int userId, int eventId)
        {
            var existingBooking = await _context.Bookings
                .AnyAsync(b => b.UserId == userId && b.EventId == eventId);
            if (existingBooking)
                return false;

            var booking = new Booking
            {
                UserId = userId,
                EventId = eventId,
                BookingDate = DateTime.Now
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}