using EventBookingSystem.Data;
using EventBookingSystem.DTOs;
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

        public async Task<Booking> BookEventAsync(int userId, int eventId)
        {
            var booking = new Booking { UserId = userId, EventId = eventId };
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
            return booking;
        }

        public async Task<List<BookingDto>> GetUserBookingsAsync(int userId)
        {
            var bookings = await _context.Bookings
                .Where(b => b.UserId == userId)
                .Include(b => b.Event)
                .Select(b => new BookingDto
                {
                    EventId = b.EventId,
                    EventName = b.Event.Name,
                    EventDate = b.Event.Date
                })
                .ToListAsync();

            return bookings;
        }

    }
}
