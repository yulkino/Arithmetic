using System.Text;

namespace Infrastructure.Configuration;

public class DatabaseConnectionOptions
{
    public const string SectionName = "ConnectionStringParameters";
    public string Host { get; set; } = string.Empty;
    public string Port { get; set; } = string.Empty;
    public string DatabasePath { get; set; } = string.Empty;
    public string UsernamePath { get; set; } = string.Empty;
    public string PasswordPath { get; set; } = string.Empty;
}