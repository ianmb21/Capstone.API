using AutoMapper;
using Capstone.Api.Services.ViewModels;
using Capstone.Data.Entities.Models;

namespace Capstone.Api.Services.Helpers
{
    public class MappingProfileService : Profile
    {
        #region Constructor
        public MappingProfileService()
        {
            CreateMap<User, UserViewModel>()
                .ForMember(dest =>
                    dest.RoleName,
                    opt => opt.MapFrom(src => src.SubRole.Role.RoleName));
            CreateMap<Request, RequestViewModel>()
                .ForMember(dest =>
                    dest.RecordTypeName,
                    opt => opt.MapFrom(src => src.RecordType.RecordName))
                .ForMember(dest =>
                    dest.DateRequested,
                    opt => opt.MapFrom(src => src.DateRequested.ToString("MMMM dd, yyyy")))
                .ForMember(dest =>
                    dest.Birthdate,
                    opt => opt.MapFrom(src => src.Birthdate.ToString("MMMM dd, yyyy")))
                .ForMember(dest =>
                    dest.DateApproved,
                    opt => opt.MapFrom(src => src.DateApproved.GetValueOrDefault().ToString("MMMM dd, yyyy")))
                .ForMember(dest =>
                    dest.DateIssued,
                    opt => opt.MapFrom(src => src.DateIssued.GetValueOrDefault().ToString("MMMM dd, yyyy")));
        }
        #endregion
    }
}
