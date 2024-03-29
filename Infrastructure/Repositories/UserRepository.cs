﻿using Application.ServiceContracts.Repositories.Read;
using Application.ServiceContracts.Repositories.Write;
using Domain.Entity;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : IUserReadRepository, IUserWriteRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

    public async ValueTask<User?> GetUserByLoginAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == email, cancellationToken);
    }

    public async ValueTask<User?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users.SingleOrDefaultAsync(u => u.Id == userId, cancellationToken);
    }

    public async ValueTask<User?> LoginUserAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == email, cancellationToken);
    }

    public async ValueTask<User> AddUserAsync(string email, string identityId,
        CancellationToken cancellationToken = default)
    {
        return (await _dbContext.Users.AddAsync(new User(email, identityId), cancellationToken)).Entity;
    }
}