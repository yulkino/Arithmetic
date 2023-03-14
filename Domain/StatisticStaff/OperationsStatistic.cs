using Domain.Entity.SettingsEntities;

namespace Domain.StatisticStaff;

/// <summary>
/// Represents a node of progress in exercises that use specific operation
/// </summary>
/// <param name="X">Exercises operation</param>
/// <param name="Y">Exercises resolve average duration with exercises that use an operation <seealso cref="X"/></param>
public sealed record OperationsStatistic(Operation X, TimeOnly Y) : IStatisticElement<Operation, TimeOnly>;