using Capstone.Data.Entities.Models;

namespace Capstone.Repositories.Interfaces
{
    public interface ICreditScoreRepository
    {
        Task<List<CreditScore>> GetCreditScore(string nationalid);
    }
}
