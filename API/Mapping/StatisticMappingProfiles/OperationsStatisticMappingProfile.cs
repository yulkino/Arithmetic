using API.DTOs.StatisticDtos;
using AutoMapper;
using Domain.StatisticStaff;
using Domain.Entity.SettingsEntities;

namespace API.Mapping.StatisticMappingProfiles;

public class OperationsStatisticMappingProfile : Profile
{
    public OperationsStatisticMappingProfile()
    {
        CreateMap<OperationsStatistic, OperationsStatisticDto>()
            .ForMember(dto => dto.Operation, o => o.MapFrom(os => os.X))
            .ForMember(dto => dto.GameAverageDuration, o => o.MapFrom(os => TimeOnly.FromTimeSpan(os.Y)));
        //TODO check if works
        //CreateMap<Diagram<OperationsStatistic, Operation, TimeOnly>, List<OperationsStatisticDto>>()
        //    .ForMember(dto => dto, o => o.MapFrom(diagram => diagram.ToList()));
    }
}