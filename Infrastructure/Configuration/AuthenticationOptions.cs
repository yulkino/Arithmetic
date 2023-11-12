namespace Infrastructure.Configuration;

public class AuthenticationOptions
{
    public const string SectionName = "Authentication";
    public string TokenUri { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string ValidIssuer { get; set; } = string.Empty;
}