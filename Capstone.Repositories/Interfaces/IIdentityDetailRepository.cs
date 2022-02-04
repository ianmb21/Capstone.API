using Capstone.Data.Entities.Models;

namespace Capstone.Repositories.Interfaces
{
    public interface IIdentityDetailRepository
    {
        Task<List<IdentityDetail>> GetIdentityDetail(string nationalid);
    }
}
