using API.DTOs.SettingsDtos.EditSettingsDtos;
using Application.Mediators.SettingsMediator.Edit;
using AutoMapper;

namespace API.Mapping.SettingsMappingProfiles;

public class OperationIdItemMappingProfile : Profile
{
    public OperationIdItemMappingProfile()
    {
        CreateMap<OperationIdDto, OperationIdItemDto>();
    }
}