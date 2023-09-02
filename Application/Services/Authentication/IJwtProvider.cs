using ErrorOr;

namespace Application.Services.Authentication;

public interface IJwtProvider
{
    Task<ErrorOr<string?>> GetForCredentialsAsync(string email, string password);
}