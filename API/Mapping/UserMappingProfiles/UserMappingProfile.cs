using API.DTOs.UserDtos;
using AutoMapper;
using Domain.Entity;

namespace API.Mapping.UserMappingProfiles;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserDto>();
    }
}