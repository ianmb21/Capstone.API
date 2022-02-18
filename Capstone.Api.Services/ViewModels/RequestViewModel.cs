using System.ComponentModel.DataAnnotations;

namespace Capstone.Api.Services.ViewModels
{
    public class RequestViewModel
    {
        public int RequestId { get; set; }
        public int UserId { get; set; }
        public string RecordTypeName { get; set; }
        public string RequestStatus { get; set; } = null!;
        public string? RecordLink { get; set; }
        public string DateRequested { get; set; }
        public string DateApproved { get; set; }
        public string NationalId { get; set; } = null!;
        public string? Remarks { get; set; }
        public string? IssuedBy { get; set; }
        public string DateIssued { get; set; }
        public string? VerifiedBy { get; set; }
        public int RecordTypeId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Birthdate { get; set; }
        public bool HasRecord { get; set; }
        public string? Purpose { get; set; }
    }
}
