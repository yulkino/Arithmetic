using API.DTOs.StatisticDtos;
using AutoMapper;
using Domain.Entity;

namespace API.Mapping.StatisticMappingProfiles;

public class StatisticMappingProfile : Profile
{
    public StatisticMappingProfile() => CreateMap<Statistic, StatisticDto>();
}