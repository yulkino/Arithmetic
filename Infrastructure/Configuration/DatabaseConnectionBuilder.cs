using Microsoft.Extensions.Configuration;
using System.Text;

namespace Infrastructure.Configuration;

public class DatabaseConnectionBuilder
{
    public DatabaseConnectionBuilder(DatabaseConnectionOptions options, IConfiguration config)
    {
        Host = options.Host;
        Port = options.Port;
        Database = config[options.DatabasePath]!;
        Username = config[options.UsernamePath]!;
        Password = config[options.PasswordPath]!;
    }
    
    public string Host { get; init; }
    public string Port { get; init; }
    public string Database { get; init; }
    public string Username { get; init; }
    public string Password { get; init; }
    
    public string GetConnectionString()
    {
        var builder = new StringBuilder();
        var parameters = new List<string>()
        {
            nameof(Host), Host,
            nameof(Port), Port,
            nameof(Database), Database,
            nameof(Username), Username,
            nameof(Password), Password
        };
        
        for(var i = 0; i < parameters.Count; i = i + 2)
        {
            builder.Append($"{parameters[i]}={parameters[i + 1]};");
        }

        return builder.ToString();
    }
}