using Capstone.Data.Entities.Models;
namespace Capstone.Repositories.Interfaces
{
    public interface IHolderRepository
    {
        Task<List<Holder>> GetHolderByName(string firstName, string lastName);
        Task<List<Holder>> GetHolderByNationalId(string nationalId);
    }
}
