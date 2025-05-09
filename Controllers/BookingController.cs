using EventBookingSystem.DTOs;
using EventBookingSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventBookingSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly BookingService _bookingService;
        public BookingController(BookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost]
        public async Task<IActionResult> Book(BookingDto dto)
        {
            try
            {
                int userId = 1;
                var booking = await _bookingService.BookEventAsync(userId, dto.EventId);
                return Ok(new { message = "Booking successful", data = booking });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Booking failed", error = ex.Message });
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserBookings(int userId)
        {
            try
            {
                var bookings = await _bookingService.GetUserBookingsAsync(userId);
                return Ok(new { message = "Bookings retrieved", data = bookings });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Failed to retrieve bookings", error = ex.Message });
            }
        }

    }
}
