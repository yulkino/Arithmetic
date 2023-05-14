using API.DTOs.ResolvedGameDtos;
using AutoMapper;
using Domain.Entity.ExerciseEntities;

namespace API.Mapping.ResolvedGameMappingProfiles;

public class ResolvedExerciseMappingProfile : Profile
{
    public ResolvedExerciseMappingProfile() => CreateMap<ResolvedExercise, ResolvedExerciseDto>()
        .ConstructUsing(source => new ResolvedExerciseDto(
            source.UserAnswer,
            source.IsCorrect, 
            source.Exercise.Answer,
            source.ElapsedTime));
}