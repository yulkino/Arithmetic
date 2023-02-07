using API.DTOs.StatisticDtos;
using AutoMapper;
using Domain.StatisticStaff;

namespace API.Mapping.StatisticMappingProfiles;

public class OperationsStatisticMappingProfile : Profile
{
    public OperationsStatisticMappingProfile()
    {
        CreateMap<OperationsStatistic, OperationsStatisticDto>()
            .ForMember(dto => dto.Operation, o => o.MapFrom(os => os.X))
            .ForMember(dto => dto.GameAverageDuration, o => o.MapFrom(os => os.Y));
    }
}