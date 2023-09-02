using Domain.Entity.SettingsEntities;

namespace Application.Mediators.DifficultyMediator.Get;

public record GetDifficultiesQuery : IOperationRequest<HashSet<Difficulty>>;