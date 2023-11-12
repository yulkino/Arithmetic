namespace Infrastructure.Configuration;

public class FirebaseAuthenticationOptions
{
    public const string SectionName = "FirebaseAuthentication";
    public string FirebaseAuthFilePath { get; set; } = string.Empty;
}