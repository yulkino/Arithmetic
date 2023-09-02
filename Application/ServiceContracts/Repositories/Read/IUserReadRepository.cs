using Domain.Entity;

namespace Application.ServiceContracts.Repositories.Read;

public interface IUserReadRepository : IReadRepository<User>
{
    ValueTask<User?> GetUserByLoginAsync(string email, CancellationToken cancellationToken = default);
    ValueTask<User?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken = default);
    ValueTask<User?> LoginUserAsync(string email, string password, CancellationToken cancellationToken = default);
}