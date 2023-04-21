using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AfroLatino.Data;
using AfroLatino.Models;

namespace Afrolatino.Controllers;

public class VideoController : Controller
{
    private readonly ApplicationDbContext _context;

    public VideoController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Video
    public async Task<IActionResult> Index()
    {
        var videos = await _context.Videos
            // .Include(c => c.Department)
            // .OrderBy(c => c.Id)
            .ToListAsync();

        return View(videos);
    }

    public IActionResult Create()
    {
        return View();
    }

    // POST: Video/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Title,DatePublication,DescriptionCourte,Type")] Video v, IFormFile SourceVideo)
    {
        if (SourceVideo != null && SourceVideo.Length > 0)
        {
            var fileVideoName = Path.GetFileName(SourceVideo.FileName);
            var fileVideoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\videos", fileVideoName);

            using (var stream1 = new FileStream(fileVideoPath, FileMode.Create))
            {
                await SourceVideo.CopyToAsync(stream1);
            }
            v.SourceVideo = fileVideoName;
            Console.WriteLine(fileVideoName);
        }
        // Create new video in DB
        _context.Add(v);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));

    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var v = await _context.Videos
            .FirstOrDefaultAsync(v => v.Id == id);
        if (v == null)
        {
            return NotFound();
        }

        return View(v);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var v = await _context.Videos.FindAsync(id);
        _context.Videos.Remove(v!);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // GET: Video/Edit/5
    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var v = await _context.Videos.FindAsync(id);
        if (v == null)
        {
            return NotFound();
        }
        return View(v);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Title,DatePublication,DescriptionCourte,Type")] Video v, IFormFile SourceVideo)
    {

        if (id != v.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                // handle file upload
                if (SourceVideo != null && SourceVideo.Length > 0)
                {
                    var fileVideoName = Path.GetFileName(SourceVideo.FileName);
                    var fileVideoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\videos", fileVideoName);

                    using (var stream = new FileStream(fileVideoPath, FileMode.Create))
                    {
                        await SourceVideo.CopyToAsync(stream);
                    }

                    v.SourceVideo = fileVideoName;
                }
                _context.Update(v);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VideoExists(v.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(v);
    }
    private bool VideoExists(int id)
    {
        return _context.Videos.Any(v => v.Id == id);
    }
}