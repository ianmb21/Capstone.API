using Capstone.Data.Entities.Models;
using Capstone.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Capstone.Repositories.Classes
{
    public class RequestRepository : IRequestRepository
    {
        #region Private Properties
        private readonly CapstoneDbContext _context;
        #endregion

        #region Constructor
        public RequestRepository(CapstoneDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Public Methods
        public async Task<List<Request>> CreateHolderRequest(List<Request> requests)
        {
            _context.Requests.AddRange(requests);
            await _context.SaveChangesAsync();

            foreach (var request in requests)
            {
                var req = await GetRequestById(request.RequestId);

                request.RecordType = req.RecordType;
            }

            return requests;
        }

        public async Task<Request> GetRequestById(int id)
        {
            var request = await _context.Requests.Include(r => r.RecordType).FirstOrDefaultAsync(r =>
            r.RequestId == id); 
            //&& r.RequestStatus == "For Verification");

            return request;
        }

        public async Task<List<Request>> GetVerifierRequests()
        {
            var request = await _context.Requests.Include(r => r.RecordType).Where(r => 
            r.RequestStatus == "For Verification").ToListAsync();

            return request;
        }

        public async Task<List<Request>> GetHolderRequest(int id)
        {
            var requests = await _context.Requests
                .Include(r => r.RecordType)
                .Where(r => r.UserId == id)
                .ToListAsync();
            
            return requests;
        }
        public async Task UpdateRequest(Request request)
        {
            var forApproval = _context.Requests.First(r =>
            r.RequestId == request.RequestId);

            forApproval.RequestStatus = request.RequestStatus;
            forApproval.DateApproved = request.DateApproved != null ? request.DateApproved : forApproval.DateApproved;
            forApproval.VerifiedBy = request.VerifiedBy != null ? request.VerifiedBy : forApproval.VerifiedBy;
            forApproval.Remarks = request.Remarks != null ? request.Remarks : forApproval.Remarks;
            forApproval.DateIssued = request.DateIssued != null ? request.DateIssued : forApproval.DateIssued;
            forApproval.IssuedBy = request.IssuedBy != null ? request.IssuedBy : forApproval.IssuedBy;

            await _context.SaveChangesAsync();
        }

        public async Task<List<Request>> GetIssuerRequest(string requestStatus)
        {
            if (String.IsNullOrEmpty(requestStatus) || requestStatus == "All")
            {
                
                return await _context.Requests.Include(r => r.RecordType).ToListAsync();
            }
            else
            {
                return await _context.Requests.Include(r => r.RecordType).Where(r => r.RequestStatus == requestStatus).ToListAsync();
            }
        }
        #endregion
    }
}
