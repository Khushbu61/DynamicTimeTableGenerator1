using DynamicTimeTableGenerator.Models;

namespace DynamicTimeTableGenerator.Repositories
{
    public interface ISubjectRepository
    {
        Task<IEnumerable<Subject>> GetAllSubjectsAsync();
        Task AddSubjectAsync(Subject subject);
    }
}
