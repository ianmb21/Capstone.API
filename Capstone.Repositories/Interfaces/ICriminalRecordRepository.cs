using Capstone.Data.Entities.Models;

namespace Capstone.Repositories.Interfaces
{
    public interface ICriminalRecordRepository
    {
        Task<List<CriminalRecord>> GetCriminalRecord(string nationalid);
    }
}
