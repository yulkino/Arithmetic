using API.DTOs.SettingsDtos.EditSettingsDtos;
using Application.Mediators.SettingsMediator.Edit;
using AutoMapper;

namespace API.Mapping.SettingsMappingProfiles;

public class DifficultyIdItemMappingProfile : Profile
{
    public DifficultyIdItemMappingProfile()
    {
        CreateMap<DifficultyIdDto, DifficultyIdItemDto>();
    }
}