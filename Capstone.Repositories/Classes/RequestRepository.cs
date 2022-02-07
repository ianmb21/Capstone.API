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

        public async Task<List<Request>> GetVerifierRequests(string requestStatus, string userId)
        {
            /*
            var request = await _context.Requests.Include(r => r.RecordType).Where(r => 
            r.RequestStatus == "For Verification" ||
            r.RequestStatus == "Revoked").ToListAsync();
            */


            var subRoleMatix = await _context.SubRoleMatrices.FromSqlInterpolated($@"select * from SubRoleMatrix where SubRoleId in (select SubRoleId from [dbo].[User] where UserId = {userId})").ToListAsync();

            var list2 = subRoleMatix.Select(a => a.RecordTypeId).ToList();

            var where2 = string.Join(",", list2);


            var x = subRoleMatix.ToList();

            if (String.IsNullOrEmpty(requestStatus) || requestStatus == "All")
            {
                return await _context.Requests.Include(r => r.RecordType).Where(r => list2.Contains(r.RecordTypeId)).ToListAsync();
            }
            else
            {
                return await _context.Requests.Include(r => r.RecordType).Where(r => list2.Contains(r.RecordTypeId)).Where(r => r.RequestStatus == requestStatus).ToListAsync();
            }
        }

        public async Task<List<Request>> GetHolderRequest(int id)
        {
            var requests = await _context.Requests
                .Include(r => r.RecordType)
                .Where(r => r.HolderId == id)
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

        
        public async Task UpdateRequestStatus(Request request)
        {
            var updatingRequest = _context.Requests.First(r =>
            r.RequestId == request.RequestId);

            if (request.RequestStatus == "For Verification" && updatingRequest.IssuedBy == null)
            {
                var issuedRequest = _context.Requests
                    .Where(r =>
                        r.UserId == updatingRequest.UserId &&
                        r.RecordTypeId == updatingRequest.RecordTypeId &&
                        r.IssuedBy != null)
                    .OrderByDescending(r => r.DateIssued)
                    .First();

                updatingRequest.IssuedBy = issuedRequest.IssuedBy;
                updatingRequest.DateIssued = issuedRequest.DateIssued;
            }

            updatingRequest.RequestStatus = request.RequestStatus;

            await _context.SaveChangesAsync();
        }

        public async Task<List<Request>> GetIssuerRequest(string requestStatus, string userId)
        {
            var subRoleMatix = await _context.SubRoleMatrices.FromSqlInterpolated($@"select * from SubRoleMatrix where SubRoleId in (select SubRoleId from [dbo].[User] where UserId = {userId})").ToListAsync();

            var list2 = subRoleMatix.Select(a => a.RecordTypeId).ToList();

            var where2 = string.Join(",", list2);


            var x = subRoleMatix.ToList();
            
            if (String.IsNullOrEmpty(requestStatus) || requestStatus == "All")
            {
                return await _context.Requests.Include(r => r.RecordType).Where(r => list2.Contains(r.RecordTypeId) ).ToListAsync();
            }
            else
            {
                return await _context.Requests.Include(r => r.RecordType).Where(r => list2.Contains(r.RecordTypeId)).Where(r => r.RequestStatus == requestStatus).ToListAsync();
            }
        }

        public async Task<List<Request>> CreateVerifierRequest(List<Request> requests)
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
        #endregion
    }
}
