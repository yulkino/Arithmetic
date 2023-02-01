using Domain.Entity;

namespace Application.ServiceContracts.Repositories.Read;

public interface IUserReadRepository : IReadRepository<User>
{
    ValueTask<User?> GetUserByLoginDataAsync(string Login, string Password, CancellationToken cancellationToken);
    ValueTask<User?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken);
}