using Application.ServiceContracts.Repositories.Read;
using Application.ServiceContracts.Repositories.Write;
using Domain.Entity;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class UserRepository : IUserReadRepository, IUserWriteRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

    public ValueTask<User?> GetUserByLoginAsync(string login, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public ValueTask<User?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public ValueTask<User?> LoginUserAsync(string login, string password, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public ValueTask<User> AddUserAsync(string login, string password, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}