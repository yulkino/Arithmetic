using Domain.Entity.SettingsEntities;

namespace Application.Mediators.DifficultyMediator;

public record GetDifficultiesQuery : IOperationRequest<HashSet<Difficulty>>;