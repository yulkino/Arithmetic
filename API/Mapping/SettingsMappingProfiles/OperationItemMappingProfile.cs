using API.DTOs.SettingsDtos;
using Application.Mediators.SettingsMediator.Edit;
using AutoMapper;

namespace API.Mapping.SettingsMappingProfiles;

public class OperationItemMappingProfile : Profile
{
    public OperationItemMappingProfile()
    {
        CreateMap<OperationDto, OperationItemDto>();
    }
}