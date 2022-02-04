﻿using Capstone.Data.Entities.Models;
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
        #endregion
    }
}
