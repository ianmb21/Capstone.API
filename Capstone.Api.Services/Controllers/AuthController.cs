using AutoMapper;
using Capstone.Api.Services.Helpers;
using Capstone.Api.Services.ViewModels;
using Capstone.Data.Entities.Models;
using Capstone.DTOs;
using Capstone.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Capstone.Api.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        #region Private Properties
        private readonly IUserRepository UserRepository;
        private readonly JwtService JwtService;
        private readonly PasswordService PasswordService;
        private readonly IMapper _mapper;
        private readonly IHolderRepository HolderRepository;
        #endregion

        #region Constructor
        public AuthController(IUserRepository userRepository,
            JwtService jwtService,
            PasswordService passwordService,
            IMapper mapper,
            IHolderRepository holderRepository)
        {
            UserRepository = userRepository;
            JwtService = jwtService;
            PasswordService = passwordService;
            _mapper = mapper;
            HolderRepository = holderRepository;
        }
        #endregion

        #region Public Methods
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO request) 
        {
            var userExist = await UserRepository.GetByUsername(request.Username);
            var role = await UserRepository.GetRoleByRoleName(request.RoleName);
            var holder = await HolderRepository.GetHolderByNationalId(request.NationalId);

            if (userExist != null)
            {
                return BadRequest("User already exists.");
            }

            if (holder == null)
            {
                return BadRequest("National Id does not exist!");
            }

            PasswordService.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User
            {
                Username = request.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                SubRoleId = role.SubRoles.First(r => r.Role.RoleName == role.RoleName).SubRoleId,
                HolderId = holder.HolderId,
            };

            return Created("success", await UserRepository.Register(user));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO request)
        {
            var user = await UserRepository.GetByUsername(request.Username);

            if (user == null)
            {
                return BadRequest("User not found!");
            }

            if (!PasswordService.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Password is incorrect!");
            }

            var jwt = JwtService.GenerateToken(user);

            Response.Cookies.Append("jwt", jwt, new CookieOptions
            {
                HttpOnly = true
            });

            var userViewModel = _mapper.Map<UserViewModel>(user);
            userViewModel.JwtToken = jwt;

            return Ok(userViewModel);
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetUser()
        {
            try
            {
                var jwtToken = Request.Cookies["jwt"];

                var token = JwtService.ValidateToken(jwtToken);

                var issuerId = token.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
                       .Select(c => c.Value).SingleOrDefault();
                var user = await UserRepository.GetById(int.Parse(issuerId));

                return Ok(user);
            }
            catch(Exception) 
            {
                return Unauthorized();
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");

            return Ok("Success");

        }

        [HttpGet("getNationalId/{id}")]
        public async Task<IActionResult> GetNationalId(int id)
        {
            var nationalId = HolderRepository.GetNationalIdByUserId(id);

            if (nationalId == null)
            {
                return BadRequest("National Id does not exist!");
            }

            return Ok(nationalId);
        }
        #endregion
    }
}
