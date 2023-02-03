using API.DTOs.GameDtos;
using AutoMapper;
using Domain.Entity.GameEntities;

namespace API.Mapping.GameMappingProfiles;

public class GameMappingProfile : Profile
{
    public GameMappingProfile()
    {
        CreateMap<Game, GameDto>();
    }
}