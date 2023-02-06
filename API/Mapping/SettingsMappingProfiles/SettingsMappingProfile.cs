using API.DTOs.SettingsDtos;
using AutoMapper;
using Domain.Entity.SettingsEntities;

namespace API.Mapping.SettingsMappingProfiles;

public class SettingsMappingProfile : Profile
{
    public SettingsMappingProfile()
    {
        CreateMap<SettingsDto, Settings>();
    }
}