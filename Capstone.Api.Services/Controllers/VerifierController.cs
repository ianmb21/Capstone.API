using AutoMapper;
using Capstone.Api.Services.ViewModels;
using Capstone.Data.Entities.Models;
using Capstone.DTOs;
using Capstone.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        #endregion

        #region Constructor
        public VerifierController(IRequestRepository requestRepository,
            IUserRepository userRepository,
            IMapper mapper) 
        {
            RequestRepository = requestRepository;
            UserRepository = userRepository;
            _mapper = mapper;
        }
        #endregion

        #region Public Methods
        [HttpGet("/{id}")]
        public async Task<IActionResult> GetRequestById(int id)
        {
            var request = await RequestRepository.GetRequestById(id);

            if (request == null)
            {
                return NotFound();
            }

            var requestViewModel = _mapper.Map<IEnumerable<RequestViewModel>>(request);

            return Ok(requestViewModel);
        }

        [HttpGet("getRequest")]
        public async Task<IActionResult> GetRequests()
        {
            var request = await RequestRepository.GetVerifierRequests();

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
        #endregion
    }
}
