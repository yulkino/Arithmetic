using Domain.Entity;

namespace Application.ServiceContracts.Repositories.Write;

public interface IUserWriteRepository : IWriteRepository<User>
{
    ValueTask<User?> AddUserAsync(string Login, string Password, string PasswordConfirmation, CancellationToken cancellationToken);
}