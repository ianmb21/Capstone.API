using Capstone.Data.Entities.Models;

namespace Capstone.Repositories.Interfaces
{
    public interface IEducationRecordRepository
    {
        Task<List<EducationRecord>> GetEducationRecord(string nationalid);
    }
}
