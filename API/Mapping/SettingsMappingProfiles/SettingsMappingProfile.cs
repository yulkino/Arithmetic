using API.DTOs.SettingsDtos.GetSettingsDtos;
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