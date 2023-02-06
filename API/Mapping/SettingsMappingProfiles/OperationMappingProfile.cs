using API.DTOs.SettingsDtos;
using AutoMapper;
using Domain.Entity.SettingsEntities;

namespace API.Mapping.SettingsMappingProfiles;

public class OperationMappingProfile : Profile
{
    public OperationMappingProfile()
    {
        CreateMap<Operation, OperationDto>();
    }
}