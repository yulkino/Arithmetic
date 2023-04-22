using Domain.Entity.SettingsEntities;

namespace Application.Services.SettingsProvider;

public class DefaultSettingsProvider : IDefaultSettingsProvider
{
    public Settings GetDefaultSettings()
    {
        return new Settings(Difficulty.Easy,
            new HashSet<Operation> { Operation.Addition, Operation.Subtraction },
            10);
    }
}