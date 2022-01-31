using Capstone.Data.Entities.Models;

namespace Capstone.Repositories.Interfaces
{
    public interface IEmploymentHistoryRepository
    {
        Task<List<EmploymentHistory>> GetEmploymentHistory(string nationalid);
    }
}
