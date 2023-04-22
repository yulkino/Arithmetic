using Domain.Entity;

namespace Application.ServiceContracts.Repositories.Read;

public interface IUserReadRepository : IReadRepository<User>
{
    ValueTask<User?> GetUserByLoginAsync(string login, CancellationToken cancellationToken = default);
    ValueTask<User?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken = default);
    ValueTask<User?> LoginUserAsync(string login, string password, CancellationToken cancellationToken = default);
}