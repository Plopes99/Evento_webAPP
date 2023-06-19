using Events_WebAPP.Server.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Events_WebAPP.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventsController: ControllerBase
{
    private readonly ApiDbContext _context;

    public EventsController(ApiDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Event>>> GetEvents()
    {
        var events = await _context.Events.ToListAsync();
        return Ok(events);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Event>> GetEventDetails(int id)
    {
        var evento = await _context.Events.FirstOrDefaultAsync(x => x.EventId == id);
        
        if (evento == null)
        {
            return NotFound();
        }

        return Ok(evento);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEvent(Event evento)
    {
        _context.Events.Add(evento);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetEventDetails), evento, evento.EventId);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEvent(Event evento, int id)
    {
        var eventExist = await _context.Events.FirstOrDefaultAsync(x => x.EventId == id);

        if (eventExist == null)
        {
            return NotFound();
        }

        TimeOnly time = TimeOnly.FromDateTime(DateTime.Now);
        
        eventExist.EventId = evento.EventId;
        eventExist.Date = evento.Date;
        eventExist.Description = evento.Description;
        eventExist.Time = time;
        eventExist.Location = evento.Location;
        eventExist.Name = evento.Name;
        eventExist.MaxCapacity = evento.MaxCapacity;
        eventExist.TicketPrice = evento.TicketPrice;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEvent(int id)
    {
        var eventExist = await _context.Events.FirstOrDefaultAsync(x => x.EventId == id);

        if (eventExist == null)
            return NotFound();

        _context.Events.Remove(eventExist);
        await _context.SaveChangesAsync();

        return NoContent();
    }

}