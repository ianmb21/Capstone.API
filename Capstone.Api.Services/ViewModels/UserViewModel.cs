namespace Capstone.Api.Services.ViewModels
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string RoleName { get; set; }
        public string JwtToken { get; set; }
        public int SubRoleId { get; set; }
    }
}
