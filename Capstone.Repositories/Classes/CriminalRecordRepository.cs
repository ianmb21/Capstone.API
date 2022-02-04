using Capstone.Data.Entities.Models;
using Capstone.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Capstone.Repositories.Classes
{
    public class CriminalRecordRepository : ICriminalRecordRepository
    {
        #region Private Properties
        private readonly CapstoneDbContext _context;
        #endregion

        #region Constructor
        public CriminalRecordRepository(CapstoneDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Public Methods
        public async Task<List<CriminalRecord>> GetCriminalRecord(string nationalid)
        {
            var criminalRecord = await _context.CriminalRecords.Where(r => r.NationalId == nationalid).ToListAsync();
            return criminalRecord;
        }
        #endregion
    }
}
