using Events_WebAPP.Server.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Events_WebAPP.Server.Controllers;

[ApiController]
[Route("api/[controller]")]

public class UsersController: ControllerBase
{
    private readonly ApiDbContext _context;

    public UsersController(ApiDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<User>>> GetUsers()
    {
        var users = await _context.Users.ToListAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUserDetails(int id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);
        
        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEvent(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUserDetails), user, user.UserId);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEvent(User user, int id)
    {
        var userExist = await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);

        if (userExist== null)
        {
            return NotFound();
        }

        userExist.UserId = user.UserId;
        userExist.Username = user.Username;
        userExist.Email = user.Email;
        userExist.Role = user.Role;
        userExist.Password= user.Password;
        userExist.PhoneNumber = user.PhoneNumber;
        userExist.Events = user.Events;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEvent(int id)
    {
        var userExist = await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);

        if (userExist == null)
            return NotFound();

        _context.Users.Remove(userExist);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}