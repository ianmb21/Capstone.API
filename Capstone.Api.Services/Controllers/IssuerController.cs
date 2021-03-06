using Capstone.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Capstone.DTOs;
using Capstone.Data.Entities.Models;
using AutoMapper;
using Capstone.Api.Services.ViewModels;


using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Capstone.Api.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssuerController : Controller
    {
        #region Private Properties
        private readonly IRequestRepository RequestRepository;
        private readonly ICreditScoreRepository CreditScoreRepository;
        private readonly ICriminalRecordRepository CriminalRecordRepository;
        private readonly IEducationRecordRepository EducationRecordRepository;
        private readonly IEmploymentHistoryRepository EmploymentHistoryRepository;
        private readonly IIdentityDetailRepository IdentityDetailRepository;
        private readonly IUserRepository UserRepository;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public IssuerController(IRequestRepository requestRepository,
            ICreditScoreRepository creditScoreRepository,
            ICriminalRecordRepository criminalRecordRepository,
            IEducationRecordRepository educationRecordRepository,
            IEmploymentHistoryRepository employmentHistoryRepository,
            IIdentityDetailRepository identityDetailRepository,
            IUserRepository userRepository,
            IMapper mapper)
        {
            RequestRepository = requestRepository;
            CreditScoreRepository = creditScoreRepository;
            CriminalRecordRepository = criminalRecordRepository;
            EducationRecordRepository = educationRecordRepository;
            EmploymentHistoryRepository = employmentHistoryRepository;
            IdentityDetailRepository = identityDetailRepository;
            UserRepository = userRepository;
            _mapper = mapper;
        }
        #endregion

        #region Public Methods
        [HttpGet("getRequest/{requestStatus}")]
        [Authorize(Roles = "Issuer")]
        public async Task<IActionResult> GetIssuerRequests(string requestStatus)
        {
            //var claims = ClaimsPrincipal.Current.Identities.First().Claims.ToList();
            //var user = claims?.FirstOrDefault(x => x.Type.Equals("NameIdentifier", StringComparison.OrdinalIgnoreCase))?.Value;

            /*
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            var name = claims.Where(p => p.ValueType == "NameIdentifier").FirstOrDefault()?.Value;
            ClaimTypes.NameIdentifier
            string username = name;
            */

            /*
              select* from[dbo].[User] where UserId = 10
              select* from SubRole
              select* from SubRoleMatrix where SubRoleId = 1
              1 is to 1 issuer
              select* from Request where RecordTypeId = 1(select * from SubRoleMatrix where SubRoleId = 1 )
             --
            select* from[dbo].[User] where UserId = 10
            select* from SubRole
            select* from SubRoleMatrix where SubRoleId in (select SubRoleId from[dbo].[User] where UserId = 10)
            1 is to 1 issuer

           select* from Request where RecordTypeId in (select RecordTypeId from SubRoleMatrix where SubRoleId in (select SubRoleId from[dbo].[User] where UserId = 10) )
           --
           --verifier example
           select* from SubRoleMatrix where SubRoleId = 7
            */
            //--------------------


            requestStatus = System.Web.HttpUtility.UrlDecode(requestStatus);
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var request = await RequestRepository.GetIssuerRequest(requestStatus, userId);

            if (request == null)
            {
                return NotFound();
            }

            var requestViewModel = _mapper.Map<IEnumerable<RequestViewModel>>(request);

            return Ok(requestViewModel);
        }

        [HttpGet("getCreditScore/{nationalid}")]
        public async Task<IActionResult> GetCreditScore(string nationalid)
        {
            var record = await CreditScoreRepository.GetCreditScore(nationalid);

            if (record.Count() == 0)
            {
                return NotFound();
            }

            return Ok(record);
        }

        [HttpGet("getCriminalRecord/{nationalid}")]
        public async Task<IActionResult> GetCriminalRecord(string nationalid)
        {
            var record = await CriminalRecordRepository.GetCriminalRecord(nationalid);

            if (record.Count() == 0)
            {
                return NotFound();
            }

            return Ok(record);
        }

        [HttpGet("getEducationRecord/{nationalid}")]
        public async Task<IActionResult> GetEducationRecord(string nationalid)
        {
            var record = await EducationRecordRepository.GetEducationRecord(nationalid);

            if (record.Count() == 0)
            {
                return NotFound();
            }
            return Ok(record);
        }

        [HttpGet("getEmploymentHistory/{nationalid}")]
        public async Task<IActionResult> GetEmploymentHistory(string nationalid)
        {
            var record = await EmploymentHistoryRepository.GetEmploymentHistory(nationalid);

            if (record.Count() == 0)
            {
                return NotFound();
            }

            return Ok(record);
        }

        [HttpGet("getIdentityDetail/{nationalid}")]
        public async Task<IActionResult> GetIdentityDetail(string nationalid)
        {
            var record = await IdentityDetailRepository.GetIdentityDetail(nationalid);

            if (record.Count() == 0)
            {
                return NotFound();
            }

            return Ok(record);
        }

        [HttpPut("updateRequest")]
        [Authorize(Roles = "Issuer")]
        public async Task<IActionResult> UpdateRequest([FromBody] RequestDTO request)
        {
            var requestExist = await RequestRepository.GetRequestById(request.RequestId);
            var issueBy = await UserRepository.GetById(request.UserId);

            if (requestExist != null)
            {
                await RequestRepository.UpdateRequest(new Request
                {
                    RequestId = request.RequestId,
                    RequestStatus = request.RequestStatus,
                    Remarks = request.Remarks,
                    DateIssued = DateTime.Now,
                    IssuedBy = issueBy.Username,
                });

                return Ok();
            }

            return NotFound();
        }
        #endregion
    }
}
