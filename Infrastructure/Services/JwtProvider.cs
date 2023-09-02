using Application.ClientErrors.Errors;
using Application.Services.Authentication;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using ErrorOr;

namespace Infrastructure.Services;

internal sealed class JwtProvider : IJwtProvider
{
    private readonly HttpClient _httpClient;

    public JwtProvider(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<ErrorOr<string?>> GetForCredentialsAsync(string email, string password)
    {
        var request = new { email, password, returnSecureToken = true };
        var response = await _httpClient.PostAsJsonAsync("", request);
        var authInformation = await response.Content.ReadFromJsonAsync<AuthInformation>();
        
        if (authInformation is { IsRegistered: false })
            return Errors.UserErrors.NotFound;
        if (!response.IsSuccessStatusCode)
            return Errors.UserErrors.Failure;
        
        return authInformation?.AuthToken;
    }

    private class AuthInformation
    {
        [JsonPropertyName("idToken")]
        public string? AuthToken { get; set; }
        [JsonPropertyName("email")]
        public string? Email { get; set; }
        [JsonPropertyName("refreshToken")]
        public string? RefreshToken { get; set; }
        [JsonPropertyName("expiresIn")]
        public string? ExpiresIn { get; set; }
        [JsonPropertyName("localId")]
        public string? Uid { get; set; }
        [JsonPropertyName("registered")]
        public bool IsRegistered { get; set; }
    }
}