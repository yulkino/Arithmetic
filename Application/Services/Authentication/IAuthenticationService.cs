namespace Application.Services.Authentication;

public interface IAuthenticationService
{
    Task<string> RegisterAsync(string email, string password, 
        CancellationToken cancellationToken = default);
    Task<bool> UserExists(string email, CancellationToken cancellationToken = default);
}