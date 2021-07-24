using System.Linq;
using AutoMapper;
using DatingApi.DTOs;
using DatingApi.Entities;
using DatingApi.Extensions;

namespace DatingApi.Helpers
{
    public class AutomapperProfiles : Profile
    {
        public AutomapperProfiles()
        {
            CreateMap<AppUser,MemberDto>()
                     .ForMember(x => x.PhotoUrl,opt => opt.MapFrom(src => src.Photos.FirstOrDefault(a => a.IsMain).Url))
                     .ForMember(x=>x.Age ,opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
            CreateMap<Photo,PhotoDto>();
            CreateMap<MemberUpdateDto,AppUser>();
        }
    }
}