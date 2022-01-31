using Capstone.Data.Entities.Models;
using Capstone.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Capstone.Repositories.Classes
{
    public class EmploymentHistoryRepository : IEmploymentHistoryRepository
    {
        #region Private Properties
        private readonly CapstoneDbContext _context;
        #endregion

        #region Constructor
        public EmploymentHistoryRepository(CapstoneDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Public Methods
        public async Task<List<EmploymentHistory>> GetEmploymentHistory(string nationalid)
        {
            var employmentHistory = await _context.EmploymentHistories.Where(r => r.NationalId == nationalid).ToListAsync();
            return employmentHistory;
        }
        #endregion
    }
}
