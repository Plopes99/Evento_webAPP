using Events_WebAPP.Server.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Events_WebAPP.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketsController : ControllerBase
{
    
    private readonly ApiDbContext _context;

    public TicketsController(ApiDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Ticket>>> GetTickets()
    {
        var tickets = await _context.Tickets.ToListAsync();
        return Ok(tickets);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Ticket>> GetTicketDetails(int id)
    {
        var ticket = await _context.Tickets.FirstOrDefaultAsync(x => x.TicketId == id);
        
        if (ticket == null)
        {
            return NotFound();
        }

        return Ok(ticket);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTicket(Ticket ticket)
    {
        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTicketDetails), ticket, ticket.TicketId);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTicket(Ticket ticket, int id)
    {
        var ticketExist = await _context.Tickets.FirstOrDefaultAsync(x => x.TicketId == id);

        if (ticketExist == null)
        {
            return NotFound();
        }

        ticketExist.TicketId = ticket.TicketId;
        ticketExist.Event = ticket.Event;
        ticketExist.EventId = ticket.EventId;
        ticketExist.TicketType = ticket.TicketType;
        ticketExist.QuantityAvailable = ticket.QuantityAvailable;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTicket(int id)
    {
        var ticketExist = await _context.Tickets.FirstOrDefaultAsync(x => x.TicketId == id);

        if (ticketExist == null)
            return NotFound();

        _context.Tickets.Remove(ticketExist);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
}