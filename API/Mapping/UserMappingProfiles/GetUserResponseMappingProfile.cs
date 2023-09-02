using API.DTOs.UserDtos;
using Application.Mediators.UserMediator.Get;
using AutoMapper;

namespace API.Mapping.UserMappingProfiles;

public class GetUserResponseMappingProfile : Profile
{
    public GetUserResponseMappingProfile() => CreateMap<GetUserResponse, LoginUserResponseDto>();
}