using Domain.Entity.SettingsEntities;

namespace Application.Mediators.DifficultyMediator;

public record GetDifficultiesQuery() : IOperationRequest<List<Difficulty>>;