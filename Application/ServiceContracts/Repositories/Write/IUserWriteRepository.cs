using Domain.Entity;

namespace Application.ServiceContracts.Repositories.Write;

public interface IUserWriteRepository : IWriteRepository<User>
{
    ValueTask<User> AddUserAsync(string login, string password, CancellationToken cancellationToken = default);
}