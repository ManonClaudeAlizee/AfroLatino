using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AfroLatino.Data;
using AfroLatino.Models;

namespace AfroLatino.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventApiController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public EventApiController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/EventApi
    public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
    {
        // Get events
        var events = _context.Events;
        // .OrderBy(s => s.LastName)
        // .ThenBy(s => s.FirstName);

        return await events.ToListAsync();
    }

    // GET: api/UserApi/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Event>> GetEvent(int id)
    {
        // Find student and related enrollments
        // SingleAsync() throws an exception if no student is found (which is possible, depending on id)
        // SingleOrDefaultAsync() is a safer choice here
        var ev = await _context.Events.FindAsync(id);
        // .Where(s => s.Id == id)
        // .Include(s => s.Enrollments)
        //.SingleOrDefaultAsync();

        if (ev == null)
            return NotFound();

        return ev;
    }

    // POST: api/EventApi
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Event>> PostEvent(Event ev)
    {
        _context.Events.Add(ev);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetEvent), new { id = ev.Id }, ev);
    }

    // DELETE: api/EventApi/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEvent(int id)
    {
        var ev = await _context.Events.FindAsync(id);
        if (ev == null)
            return NotFound();

        _context.Events.Remove(ev);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // PUT: api/EventApi/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutEvent(int id, Event ev)
    {
        if (id != ev.Id)
            return BadRequest();

        _context.Entry(ev).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!EventExists(id))
                return NotFound();
            else
                throw;
        }
        return NoContent();
    }

    // Returns true if an event with specified id already exists
    private bool EventExists(int id)
    {
        return _context.Events.Any(e => e.Id == id);
    }

}
