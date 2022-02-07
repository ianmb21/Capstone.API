using Capstone.Data.Entities.Models;
using Capstone.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Capstone.DTOs;
using Capstone.Api.Services.ViewModels;
using AutoMapper;

namespace Capstone.Api.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Holder")]
    //[Authorize]
    public class HolderController : Controller
    {
        #region Private Properties
        private readonly IRequestRepository RequestRepository;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public HolderController(IRequestRepository requestRepository,
            IMapper mapper)
        {
            RequestRepository = requestRepository;
            _mapper = mapper;
        }
        #endregion

        #region Public Methods
        [HttpPost("createRequest")]
        public async Task<IActionResult> CreateHolderRequest([FromBody] RequestDTO req)
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
                    RequestStatus = "New Request"
                });
            }

            var requestList = _mapper.Map<IEnumerable<RequestViewModel>>(await RequestRepository.CreateHolderRequest(requests));
            return Ok(requestList);
        }

        [HttpPut("updateRequestStatus")]
        public async Task<IActionResult> UpdateRequestStatus([FromBody] RequestStatusDTO request)
        {
            var requestExist = await RequestRepository.GetRequestById(request.RequestId);

            if (requestExist != null)
            {
                await RequestRepository.UpdateRequestStatus(new Request
                {
                    RequestId = request.RequestId,
                    RequestStatus = request.RequestStatus
                });

                return Ok();
            }

            return NotFound();
        }


        [HttpGet("getRequest/{id}/{requestStatus}")]
        public async Task<IActionResult> GetHolderRequest(int id, string requestStatus)
        {
            requestStatus = System.Web.HttpUtility.UrlDecode(requestStatus);
            var request = await RequestRepository.GetHolderRequest(id, requestStatus);

            if (request == null)
            {
                return NotFound();
            }

            var newRequests = request.Where(nr => nr.RequestStatus == "Request Confirmation").ToList();

            var requestViewModel = _mapper.Map<IEnumerable<RequestViewModel>>(request);

            foreach (var newRequest in newRequests)
            {
                var hasRecord = request.Any(hr => hr.IssuedBy != null && hr.RecordTypeId == newRequest.RecordTypeId);

                requestViewModel.FirstOrDefault(rvm => rvm.RequestId == newRequest.RequestId).HasRecord = hasRecord;
            }

            return Ok(requestViewModel);
        }
        #endregion
    }
}
