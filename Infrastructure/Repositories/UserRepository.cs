using Application.ServiceContracts.Repositories.Read;
using Application.ServiceContracts.Repositories.Write;
using Domain.Entity;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : IUserReadRepository, IUserWriteRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

    public async ValueTask<User?> GetUserByLoginAsync(string login, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users.SingleOrDefaultAsync(u => u.Login == login, cancellationToken);
    }

    public async ValueTask<User?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users.SingleOrDefaultAsync(u => u.Id == userId, cancellationToken);
    }

    public async ValueTask<User?> LoginUserAsync(string login, string password, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users.SingleOrDefaultAsync(u => u.Login == login && u.PasswordHash == password, cancellationToken);
    }

    public async ValueTask<User> AddUserAsync(string login, string password, CancellationToken cancellationToken = default)
    {
        return (await _dbContext.Users.AddAsync(new User(login, password), cancellationToken)).Entity;
    }
}