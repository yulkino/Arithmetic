using API.DTOs.StatisticDtos;
using AutoMapper;
using Domain.StatisticStaff;

namespace API.Mapping.StatisticMappingProfiles;

public class GameStatisticMappingProfile : Profile
{
    public GameStatisticMappingProfile()
    {
        CreateMap<GameStatistic, GameStatisticDto>();
    }
}