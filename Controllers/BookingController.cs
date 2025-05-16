using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EventBookingSystem.DTOs;
using EventBookingSystem.Services;

namespace EventBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BookingsController : ControllerBase
    {
        private readonly BookingService _bookingService;

        public BookingsController(BookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] BookingDto model)
        {
            var userId = int.Parse(User.Identity.Name);
            var created = await _bookingService.CreateBookingAsync(userId, model.EventId);
            if (!created)
                return BadRequest("Booking failed.");
            return Ok();
        }
    }
}