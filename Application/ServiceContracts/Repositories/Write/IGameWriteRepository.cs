﻿using Domain.Entity;
using Domain.Entity.GameEntities;
using Domain.Entity.SettingsEntities;

namespace Application.ServiceContracts.Repositories.Write;

public interface IGameWriteRepository : IWriteRepository<Game>
{
    ValueTask<Game> CreateAsync(User user, Settings settings, DateTime creationDate, CancellationToken cancellationToken = default);
}