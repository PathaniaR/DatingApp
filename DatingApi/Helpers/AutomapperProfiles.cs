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
            CreateMap<RegisterDto,AppUser>();
            CreateMap<Message, MessageDto>()
                .ForMember(dest => dest.SenderPhotoUrl, opt => opt.MapFrom(src => src.Sender.Photos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(dest => dest.RecipientPhotoUrl, opt => opt.MapFrom(src => src.Recipient.Photos.FirstOrDefault(x => x.IsMain).Url));
        }
    }
}