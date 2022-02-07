using Capstone.Data.Entities.Models;

namespace Capstone.Repositories.Interfaces
{
    public interface IRequestRepository
    {
        Task<List<Request>> GetVerifierRequests(string requestStatus, string userId);
        Task UpdateRequest(Request request);
        Task<Request> GetRequestById(int id);
        Task<List<Request>> CreateHolderRequest(List<Request> requests);
        Task<List<Request>> GetHolderRequest(int id, string requestStatus);
        Task<List<Request>> GetIssuerRequest(string requestStatus, string userId);
        Task UpdateRequestStatus(Request request);
        Task<List<Request>> CreateVerifierRequest(List<Request> requests);
    }
}
