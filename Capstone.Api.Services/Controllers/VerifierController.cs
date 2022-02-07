using AutoMapper;
using Capstone.Api.Services.ViewModels;
using Capstone.Data.Entities.Models;
using Capstone.DTOs;
using Capstone.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Capstone.Api.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Verifier")]
    public class VerifierController : Controller
    {
        #region Private Property
        private readonly IRequestRepository RequestRepository;
        private readonly IUserRepository UserRepository;
        private readonly IMapper _mapper;
        private readonly IHolderRepository HolderRepository;
        #endregion

        #region Constructor
        public VerifierController(IRequestRepository requestRepository,
            IUserRepository userRepository,
            IMapper mapper,
            IHolderRepository holderRepository) 
        {
            RequestRepository = requestRepository;
            UserRepository = userRepository;
            _mapper = mapper;
            HolderRepository = holderRepository;
        }
        #endregion

        #region Public Methods
        [HttpGet("/{id}")]
        public async Task<IActionResult> GetRequestById(int id)
        {
            List<Request> requests = new List<Request>();
            var request = await RequestRepository.GetRequestById(id);

            if (request == null)
            {
                return NotFound();
            }

            requests.Add(request);

            var requestViewModel = _mapper.Map<IEnumerable<RequestViewModel>>(requests);

            return Ok(requestViewModel);
        }

        [HttpGet("getRequest/{requestStatus}")]
        public async Task<IActionResult> GetRequests(string requestStatus="All")
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var request = await RequestRepository.GetVerifierRequests(requestStatus, userId);
            requestStatus = System.Web.HttpUtility.UrlDecode(requestStatus);

            if (request == null)
            {
                return NotFound();
            }

            var requestViewModel = _mapper.Map<IEnumerable<RequestViewModel>>(request);

            return Ok(requestViewModel);
        }

        [HttpPut("updateRequest")]
        public async Task<IActionResult> UpdateRequest([FromBody] RequestDTO request)
        {
            var requestExist = await RequestRepository.GetRequestById(request.RequestId);
            var verifiedBy = await UserRepository.GetById(request.UserId);

            if (requestExist != null)
            {
                await RequestRepository.UpdateRequest(new Request 
                {
                    RequestId = request.RequestId,
                    RequestStatus = request.RequestStatus,
                    DateApproved = request.RequestStatus == "Approved" ? DateTime.Now : null,
                    VerifiedBy = verifiedBy.Username,
                    Remarks = request.Remarks,
                });

                return Ok();
            }

            return NotFound();
        }

        [HttpPost("searchHolder")]
        public async Task<IActionResult> SearchHolder([FromBody] SearchDTO search)
        {
            var request = await HolderRepository.GetHolderByName(search.FirstName, search.LastName);

            if (request == null)
            {
                return NotFound();
            }

            return Ok(request);
        }

        [HttpPost("createRequest")]
        public async Task<IActionResult> CreateVerifierRequest([FromBody] RequestDTO req)
        {
            List<Request> requests = new List<Request>();

            foreach (int id in req.RecordTypeId)
            {
                requests.Add(new Request
                {
                    UserId = req.UserId,
                    NationalId = req.NationalId,
                    RecordTypeId = id,
                    FirstName = req.FirstName,
                    LastName = req.LastName,
                    Birthdate = req.Birthdate,
                    DateRequested = DateTime.Now,
                    RequestStatus = "Request Confirmation",
                    Purpose = req.Remarks,
                    HolderId = req.HolderId,
                });
            }

            var requestList = _mapper.Map<IEnumerable<RequestViewModel>>(await RequestRepository.CreateVerifierRequest(requests));
            return Ok(requestList);
        }

        [HttpGet("getRecordType/{id}")]
        public async Task<IActionResult> GetRecordType(int id)
        {
            var subRoleID = await UserRepository.GetRecordTypeByUserId(id);

            return Ok(subRoleID);
        }
        #endregion
    }
}
