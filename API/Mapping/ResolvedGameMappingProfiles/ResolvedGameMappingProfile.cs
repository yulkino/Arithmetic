using API.DTOs.ResolvedGameDtos;
using AutoMapper;
using Domain.Entity.GameEntities;

namespace API.Mapping.ResolvedGameMappingProfiles;

public class ResolvedGameMappingProfile : Profile
{
    public ResolvedGameMappingProfile()
    {
        CreateMap<ResolvedGame, ResolvedGameDto>();
    }
}