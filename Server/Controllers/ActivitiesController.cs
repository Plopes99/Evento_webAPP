using Events_WebAPP.Server.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Events_WebAPP.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ActivitiesController : ControllerBase
{
    private readonly ApiDbContext _context;

    public ActivitiesController(ApiDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Activity>>> GetActivities()
    {
        var activities = await _context.Activities.ToListAsync();
        return Ok(activities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Activity>> GetActivityDetails(int id)
    {
        var activity = await _context.Activities.FirstOrDefaultAsync(x => x.ActivityId == id);
        
        if (activity == null)
        {
            return NotFound();
        }

        return Ok(activity);
    }

    [HttpPost]
    public async Task<IActionResult> CreateActivity(Activity activity)
    {
        _context.Activities.Add(activity);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetActivityDetails), activity, activity.ActivityId);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateActivity(Activity activity, int id)
    {
        var activityExist = await _context.Activities.FirstOrDefaultAsync(x => x.ActivityId == id);

        if (activityExist == null)
        {
            return NotFound();
        }

        TimeOnly time = TimeOnly.FromDateTime(DateTime.Now);
        DateOnly date = DateOnly.FromDateTime(DateTime.Now);
        
        activityExist.ActivityId = activity.ActivityId;
        activityExist.Time = time;
        activity.Date = date;
        activityExist.Event = activity.Event;
        activityExist.Name = activity.Name;
        activityExist.Description = activity.Description;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteActivity(int id)
    {
        var activityExist = await _context.Activities.FirstOrDefaultAsync(x => x.ActivityId == id);

        if (activityExist == null)
            return NotFound();

        _context.Activities.Remove(activityExist);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
}