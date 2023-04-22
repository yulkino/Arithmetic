using API.DTOs.ResolvedGameDtos;
using AutoMapper;
using Domain.Entity.ExerciseEntities;

namespace API.Mapping.ResolvedGameMappingProfiles;

public class ResolvedExerciseMappingProfile : Profile
{
    public ResolvedExerciseMappingProfile() => CreateMap<ResolvedExercise, ResolvedExerciseDto>()
        .ForMember(dto => dto.CorrectAnswer, o => o.MapFrom(r => r.Exercise.Answer));
}