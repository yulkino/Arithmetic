using Domain.Entity.SettingsEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Data;

internal class StaticEntityInterceptor : IMaterializationInterceptor
{
    public object InitializedInstance(MaterializationInterceptionData materializationData, object instance)
    {
        if (instance is Operation or Difficulty)
        {
            materializationData.Context.Entry(instance).State = EntityState.Unchanged;
        }

        return instance;
    }
}
