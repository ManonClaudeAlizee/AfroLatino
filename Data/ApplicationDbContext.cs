using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AfroLatino.Models;

namespace AfroLatino.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public DbSet<Event> Events { get; set; } = null!;
    public DbSet<Video> Videos { get; set; } = null!;
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    { }
}
