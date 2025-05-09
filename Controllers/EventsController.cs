using EventBookingSystem.Models;
using EventBookingSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventBookingSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly EventService _eventService;
        public EventsController(EventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var events = await _eventService.GetAllEventsAsync();
                return Ok(new { message = "Events retrieved", data = events });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Failed to retrieve events", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(Event e)
        {
            try
            {
                var created = await _eventService.CreateEventAsync(e);
                return Ok(new { message = "Event created successfully", data = created });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Event creation failed", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _eventService.DeleteEventAsync(id);
                if (!result)
                    return NotFound(new { message = "Event not found" });

                return Ok(new { message = "Event deleted successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Event deletion failed", error = ex.Message });
            }
        }

    }
}
