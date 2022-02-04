using Capstone.Data.Entities.Models;
using Capstone.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Capstone.Repositories.Classes
{
    public class CreditScoreRepository : ICreditScoreRepository
    {
        #region Private Properties
        private readonly CapstoneDbContext _context;
        #endregion

        #region Constructor
        public CreditScoreRepository(CapstoneDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Public Methods
        public async Task<List<CreditScore>> GetCreditScore(string nationalid)
        {
            var creditScore = await _context.CreditScores.Where(r => r.NationalId == nationalid).ToListAsync();
            return creditScore;
        }
        #endregion
    }
}
