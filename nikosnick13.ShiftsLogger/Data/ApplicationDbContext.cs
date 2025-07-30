using Microsoft.EntityFrameworkCore;
using nikosnick13.ShiftsLoggerAPI.Model;

namespace nikosnick13.ShiftsLoggerAPI.Data;

public class ApplicationDbContext:DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options) { }
     


    public DbSet<Shift> Shift { get; set; }
}
