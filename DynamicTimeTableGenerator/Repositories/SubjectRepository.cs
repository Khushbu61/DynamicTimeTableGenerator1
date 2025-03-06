using Microsoft.EntityFrameworkCore;
using DynamicTimeTableGenerator.Models;

namespace DynamicTimeTableGenerator.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly TimetableDbContext _context;

        public SubjectRepository(TimetableDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Subject>> GetAllSubjectsAsync()
        {
            return await _context.Subjects.ToListAsync();
        }

        public async Task AddSubjectAsync(Subject subject)
        {
            _context.Subjects.Add(subject);
            await _context.SaveChangesAsync();
        }
    }
}
