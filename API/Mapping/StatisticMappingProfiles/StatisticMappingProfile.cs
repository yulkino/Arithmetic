using API.DTOs.StatisticDtos;
using AutoMapper;
using Domain.Entity;

namespace API.Mapping.StatisticMappingProfiles;

public class StatisticMappingProfile : Profile
{
    public StatisticMappingProfile() => 
        CreateMap<Statistic, StatisticDto>()
            .ForMember(dto => dto.GameStatistic, o => o.MapFrom(s => s.GameStatistic))
            .ForMember(dto => dto.OperationsStatistic, o => o.MapFrom(s => s.OperationsStatistic))
            .ForMember(dto => dto.ExerciseProgressStatistic, o => o.MapFrom(s => s.ExerciseProgressStatistic));
}