using Capstone.Data.Entities.Models;
using Capstone.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Capstone.Repositories.Classes
{
    public class HolderRepository : IHolderRepository
    {
        #region Private Properties
        private readonly CapstoneDbContext _context;
        #endregion

        #region Constructor
        public HolderRepository(CapstoneDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Public Methods
        public async Task<List<Holder>> GetHolderByName(string firstName, string lastName)
        {
            var holderDetail = await _context.Holders.Where(r => r.FirstName == firstName && r.LastName == lastName).ToListAsync();
            return holderDetail;
        }
        public async Task<Holder> GetHolderByNationalId(string nationalId)
        {
            var holderDetail = await _context.Holders.FirstOrDefaultAsync(r => r.NationalId == nationalId);
            return holderDetail;
        }

        public async Task<string> GetNationalIdByUserId(int id)
        {
            var holder = _context.Holders.FirstOrDefault(h => h.HolderId == _context.Users.FirstOrDefault(u => u.UserId == id).HolderId);
            return holder.NationalId;
        }
        #endregion
    }
}
