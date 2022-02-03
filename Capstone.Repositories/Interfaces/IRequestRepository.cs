using Capstone.Data.Entities.Models;

namespace Capstone.Repositories.Interfaces
{
    public interface IRequestRepository
    {
        Task<List<Request>> GetVerifierRequests();
        Task UpdateRequest(Request request);
        Task<Request> GetRequestById(int id);
        Task<List<Request>> CreateHolderRequest(List<Request> requests);
        Task<List<Request>> GetHolderRequest(int id);
        Task<List<Request>> GetIssuerRequest(string requestStatus);
    }
}
