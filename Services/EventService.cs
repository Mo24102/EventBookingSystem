using Microsoft.EntityFrameworkCore;
using EventBookingSystem.Data;
using EventBookingSystem.Models;
using System.Security.Claims;
using EventBookingSystem.DTOs;

namespace EventBookingSystem.Services
{
    public class EventService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EventService(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<object> GetEventsAsync(int page, int pageSize)
        {
            var userId = GetUserId();
            var query = _context.Events
                .Include(e => e.Category)
                .Select(e => new EventDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Description = e.Description,
                    CategoryId = e.CategoryId,
                    CategoryName = e.Category.Name,
                    Date = e.Date,
                    Venue = e.Venue,
                    Price = e.Price,
                    ImageUrl = e.ImageUrl,
                    IsBooked = userId != 0 && _context.Bookings.Any(b => b.EventId == e.Id && b.UserId == userId)
                });

            var total = await query.CountAsync();
            var events = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new { Events = events, HasNextPage = page * pageSize < total };
        }

        public async Task<EventDto> GetEventByIdAsync(int id)
        {
            var userId = GetUserId();
            return await _context.Events
                .Include(e => e.Category)
                .Where(e => e.Id == id)
                .Select(e => new EventDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Description = e.Description,
                    CategoryId = e.CategoryId,
                    CategoryName = e.Category.Name,
                    Date = e.Date,
                    Venue = e.Venue,
                    Price = e.Price,
                    ImageUrl = e.ImageUrl,
                    IsBooked = userId != 0 && _context.Bookings.Any(b => b.EventId == e.Id && b.UserId == userId)
                })
                .FirstOrDefaultAsync();
        }

        public async Task<EventDto> CreateEventAsync(EventDto model)
        {
            
            var category = await _context.Categories.FindAsync(model.CategoryId);
            if (category == null)
                throw new InvalidOperationException("Invalid category ID.");

            var eventEntity = new Event
            {
                Name = model.Name,
                Description = model.Description,
                CategoryId = model.CategoryId,
                Date = model.Date,
                Venue = model.Venue,
                Price = model.Price,
                ImageUrl = model.ImageUrl
            };

            _context.Events.Add(eventEntity);
            await _context.SaveChangesAsync();

            model.Id = eventEntity.Id;
            model.CategoryName = category.Name;
            return model;
        }

        public async Task<bool> UpdateEventAsync(int id, EventDto model)
        {
            var eventEntity = await _context.Events.FindAsync(id);
            if (eventEntity == null)
                return false;

            
            var category = await _context.Categories.FindAsync(model.CategoryId);
            if (category == null)
                throw new InvalidOperationException("Invalid category ID.");

            eventEntity.Name = model.Name;
            eventEntity.Description = model.Description;
            eventEntity.CategoryId = model.CategoryId;
            eventEntity.Date = model.Date;
            eventEntity.Venue = model.Venue;
            eventEntity.Price = model.Price;
            eventEntity.ImageUrl = model.ImageUrl;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteEventAsync(int id)
        {
            var eventEntity = await _context.Events.FindAsync(id);
            if (eventEntity == null)
                return false;

            
            var hasBookings = await _context.Bookings.AnyAsync(b => b.EventId == id);
            if (hasBookings)
                throw new InvalidOperationException("Cannot delete event with existing bookings.");

            _context.Events.Remove(eventEntity);
            await _context.SaveChangesAsync();
            return true;
        }

        private int GetUserId()
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.TryParse(userIdClaim, out var userId) ? userId : 0;
        }
    }
}