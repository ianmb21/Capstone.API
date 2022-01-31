using Capstone.Data.Entities.Models;
using Capstone.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Capstone.Repositories.Classes
{
    public class IdentityDetailRepository : IIdentityDetailRepository
    {
        #region Private Properties
        private readonly CapstoneDbContext _context;
        #endregion

        #region Constructor
        public IdentityDetailRepository(CapstoneDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Public Methods
        public async Task<List<IdentityDetail>> GetIdentityDetail(string nationalid)
        {
            var identityDetail = await _context.IdentityDetails.Where(r => r.NationalId == nationalid).ToListAsync();
            return identityDetail;
        }
        #endregion
    }
}
