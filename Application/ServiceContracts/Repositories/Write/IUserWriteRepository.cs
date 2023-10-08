using Domain.Entity;

namespace Application.ServiceContracts.Repositories.Write;

public interface IUserWriteRepository : IWriteRepository<User>
{
    ValueTask<User> AddUserAsync(string email, string identityId,
        CancellationToken cancellationToken = default);
}