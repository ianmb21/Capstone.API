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


        [HttpGet("getRequest/{id}")]
        public async Task<IActionResult> GetHolderRequest(int id)
        {
            var request = await RequestRepository.GetHolderRequest(id);

            if (request == null)
            {
                return NotFound();
            }

            var requestViewModel = _mapper.Map<IEnumerable<RequestViewModel>>(request);

            return Ok(requestViewModel);
        }
        #endregion
    }
}
