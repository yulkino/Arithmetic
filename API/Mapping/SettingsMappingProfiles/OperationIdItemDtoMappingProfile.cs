using Application.Mediators.SettingsMediator.Edit;
using AutoMapper;

namespace API.Mapping.SettingsMappingProfiles;

public class OperationIdItemDtoMappingProfile : Profile
{
    public OperationIdItemDtoMappingProfile()
    {
        CreateMap<OperationIdItemDto, Guid>()
            .ForMember(g => g, o => o.MapFrom(op => op.Id));
    }
}