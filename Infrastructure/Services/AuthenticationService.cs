using Application.Services.Authentication;
using FirebaseAdmin.Auth;

namespace Infrastructure.Services;

internal sealed class AuthenticationService : IAuthenticationService
{
    public async Task<string> RegisterAsync(string email, string password, 
        CancellationToken cancellationToken = default)
    {
        var userArg = new UserRecordArgs() { Email = email, Password = password, };
        var userRecord = await FirebaseAuth.DefaultInstance.CreateUserAsync(userArg, cancellationToken);
        return userRecord.Uid;
    }

    public async Task<bool> UserExists(string email, CancellationToken cancellationToken = default)
    {
        try
        {
            await FirebaseAuth.DefaultInstance.GetUserByEmailAsync(email, cancellationToken);
            return true;
        }
        catch (FirebaseAuthException)
        {
            return false;
        }
    }
}