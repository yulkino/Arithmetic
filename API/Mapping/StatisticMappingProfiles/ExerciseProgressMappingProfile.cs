using API.DTOs.StatisticDtos;
using AutoMapper;
using Domain.StatisticStaff;

namespace API.Mapping.StatisticMappingProfiles;

public class ExerciseProgressMappingProfile : Profile
{
    public ExerciseProgressMappingProfile() => CreateMap<ExerciseProgressStatistic, ExerciseProgressStatisticDto>()
        .ForMember(dto => dto.ExercisesResolveDate, o => o.MapFrom(eps => eps.X))
        .ForMember(dto => dto.ExercisesResolveAverageDuration, o => o.MapFrom(eps => TimeOnly.FromTimeSpan(eps.Y)));
}