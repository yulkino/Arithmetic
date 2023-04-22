﻿using Application.ServiceContracts.Repositories.Read.SettingsReadRepositories;
using Application.ServiceContracts.Repositories.Write;
using Domain.Entity.SettingsEntities;
using Infrastructure.Data;

namespace Infrastructure.Repositories.SettingsRepositories;

public class SettingsRepository : ISettingsReadRepository
{
    private readonly ApplicationDbContext _dbContext;

    public SettingsRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

    public ValueTask<Settings?> GetSettingsAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}