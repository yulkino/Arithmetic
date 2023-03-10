using Application.Mediators.SettingsMediator.Edit;
using AutoMapper;

namespace API.Mapping.SettingsMappingProfiles;

public class DifficultyIdItemDtoMappingProfile : Profile
{
    public DifficultyIdItemDtoMappingProfile()
    {
        CreateMap<DifficultyIdItemDto, Guid>()
            .ForMember(g => g, o => o.MapFrom(d => d.Id));
    }
}