using Microsoft.EntityFrameworkCore;
using MoviesAPI.Models;

namespace MoviesAPI.Data;

public class APIContext : DbContext
{
    public APIContext(DbContextOptions<APIContext> opts)
        : base(opts){}
    
    public DbSet<User> Users { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Specialty> Specialties { get; set; }    
    public DbSet<Weekdays> Weekdays { get; set; }    
    public DbSet<EmailResetToken> ResetTokens { get; set; }
}
