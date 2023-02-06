using API.DTOs.SettingsDtos;
using Application.Mediators.SettingsMediator.Edit;
using AutoMapper;

namespace API.Mapping.SettingsMappingProfiles;

public class DifficultyItemMappingProfile : Profile
{
    public DifficultyItemMappingProfile()
    {
        CreateMap<DifficultyDto, DifficultyItemDto>();
    }
}