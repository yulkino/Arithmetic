using Domain.Entity.SettingsEntities;

namespace Application.Services.SettingsProvider;

public interface IDefaultSettingsProvider
{
    Settings GetDefaultSettings();
}