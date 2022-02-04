using Capstone.Data.Entities.Models;
namespace Capstone.Repositories.Interfaces
{
    public interface IHolderRepository
    {
        Task<List<Holder>> GetHolderByName(string firstName, string lastName);
    }
}
