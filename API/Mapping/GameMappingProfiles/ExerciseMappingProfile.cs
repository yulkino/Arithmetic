using API.DTOs.GameDtos;
using AutoMapper;
using Domain.Entity.ExerciseEntities;

namespace API.Mapping.GameMappingProfiles;

public class ExerciseMappingProfile : Profile
{
    public ExerciseMappingProfile()
    {
        CreateMap<Exercise, ExerciseDto>();
    }
}