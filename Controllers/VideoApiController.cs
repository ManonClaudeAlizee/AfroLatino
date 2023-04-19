using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AfroLatino.Data;
using AfroLatino.Models;

namespace AfroLatino.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VideoApiController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public VideoApiController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/VideoApi
    public async Task<ActionResult<IEnumerable<Video>>> GetVideos()
    {
        // Get videos
        var videos = _context.Videos;
        // .OrderBy(s => s.LastName)
        // .ThenBy(s => s.FirstName);

        return await videos.ToListAsync();
    }

    // GET: api/VideoApi/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Event>> GetVideo(int id)
    {
        // Find student and related enrollments
        // SingleAsync() throws an exception if no student is found (which is possible, depending on id)
        // SingleOrDefaultAsync() is a safer choice here
        var v = await _context.Events.FindAsync(id);
        // .Where(s => s.Id == id)
        // .Include(s => s.Enrollments)
        //.SingleOrDefaultAsync();

        if (v == null)
            return NotFound();

        return v;
    }

    // POST: api/VideoApi
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Event>> PostVideo(Video v)
    {
        _context.Videos.Add(v);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetVideo), new { id = v.Id }, v);
    }

    // DELETE: api/VideoApi/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVideo(int id)
    {
        var v = await _context.Videos.FindAsync(id);
        if (v == null)
            return NotFound();

        _context.Videos.Remove(v);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // PUT: api/VideoApi/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutVideo(int id, Video v)
    {
        if (id != v.Id)
            return BadRequest();

        _context.Entry(v).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!VideoExists(id))
                return NotFound();
            else
                throw;
        }
        return NoContent();
    }

    // Returns true if an event with specified id already exists
    private bool VideoExists(int id)
    {
        return _context.Videos.Any(v => v.Id == id);
    }

}
