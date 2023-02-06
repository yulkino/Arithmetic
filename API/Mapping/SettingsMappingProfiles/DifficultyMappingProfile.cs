using API.DTOs.SettingsDtos;
using AutoMapper;
using Domain.Entity.SettingsEntities;

namespace API.Mapping.SettingsMappingProfiles;

public class DifficultyMappingProfile : Profile
{
    public DifficultyMappingProfile()
    {
        CreateMap<Difficulty, DifficultyDto>();
    }
}