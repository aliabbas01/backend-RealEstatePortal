using AutoMapper;
using RealEstatePortal.DTOs;
using RealEstatePortal.Models;

namespace RealEstatePortal.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Property to PropertyDto mapping
            CreateMap<Property, PropertyDto>()
                .ForMember(dest => dest.ImageUrls, opt => opt.MapFrom<JsonToListResolver>())
                .ForMember(dest => dest.IsFavorite, opt => opt.Ignore());

            // Property to PropertyDetailsDto mapping
            CreateMap<Property, PropertyDetailsDto>()
                .ForMember(dest => dest.ImageUrls, opt => opt.MapFrom<JsonToListResolver>())
                .ForMember(dest => dest.IsFavorite, opt => opt.Ignore());

            // User mappings
            CreateMap<UserRegisterDto, User>();
        }
    }
}