using Events_WebAPP.Server.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Events_WebAPP.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RegistrationsController : ControllerBase
{
    private readonly ApiDbContext _context;

    public RegistrationsController(ApiDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<Registration>>> GetRegistrations()
    {
        var regist = await _context.Registrations.ToListAsync();
        return Ok(regist);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Registration>> GetRegistrationDetails(int id)
    {
        var regist = await _context.Registrations.FirstOrDefaultAsync(x => x.RegistrationId == id);
        
        if (regist == null)
        {
            return NotFound();
        }

        return Ok(regist);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRegistration(Registration regist)
    {
        _context.Registrations.Add(regist);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetRegistrationDetails), regist, regist.RegistrationId);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRegistration(Registration regist, int id)
    {
        var registrationExist = await _context.Registrations.FirstOrDefaultAsync(x => x.RegistrationId == id);

        if (registrationExist == null)
        {
            return NotFound();
        }

        TimeOnly time = TimeOnly.FromDateTime(DateTime.Now);
        
        registrationExist.RegistrationId = regist.RegistrationId;
        registrationExist.ParticipantId = regist.ParticipantId;
     

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRegistration(int id)
    {
        var registrationExist = await _context.Registrations.FirstOrDefaultAsync(x => x.RegistrationId == id);

        if (registrationExist == null)
            return NotFound();

        _context.Registrations.Remove(registrationExist);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
}