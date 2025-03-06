using Microsoft.EntityFrameworkCore;

namespace DynamicTimeTableGenerator.Models
{
    public class TimetableDbContext : DbContext
    {
        public TimetableDbContext(DbContextOptions<TimetableDbContext> options) : base(options) { }
        public DbSet<Subject> Subjects { get; set; }
    }
}
