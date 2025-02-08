using ApplicationServices.Services.DTO;
using AutoMapper;
using Core.DominModels.UserAggregate;

namespace ApplicationServices.Mapper
{
    public class ProfileMapper : Profile
    {
        public ProfileMapper()
        {
            CreateMap<ProfileDTO, UserProfile>();
            CreateMap<UserProfile, ProfileDTO>();
        }
    }
}
