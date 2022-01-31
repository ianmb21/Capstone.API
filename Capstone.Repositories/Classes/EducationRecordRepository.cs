using Capstone.Data.Entities.Models;
using Capstone.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Capstone.Repositories.Classes
{
    public class EducationRecordRepository : IEducationRecordRepository
    {
        #region Private Properties
        private readonly CapstoneDbContext _context;
        #endregion

        #region Constructor
        public EducationRecordRepository(CapstoneDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Public Methods
        public async Task<List<EducationRecord>> GetEducationRecord(string nationalid)
        {
            var educationRecord = await _context.EducationRecords.Where(r => r.NationalId == nationalid).ToListAsync();
            return educationRecord;
        }
        #endregion
    }
}
