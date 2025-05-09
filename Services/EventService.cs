using EventBookingSystem.Data;
using EventBookingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace EventBookingSystem.Services
{
    public class EventService
    {
        private readonly AppDbContext _context;
        public EventService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Event>> GetAllEventsAsync() => await _context.Events.ToListAsync();
        public async Task<Event> GetEventByIdAsync(int id) => await _context.Events.FindAsync(id);
        public async Task<Event> CreateEventAsync(Event e)
        {
            _context.Events.Add(e);
            await _context.SaveChangesAsync();
            return e;
        }
        public async Task<bool> DeleteEventAsync(int id)
        {
            var e = await _context.Events.FindAsync(id);
            if (e == null) return false;
            _context.Events.Remove(e);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
