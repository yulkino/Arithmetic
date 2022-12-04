namespace Infrastructure.Configuration;

public class DatabaseConnectionOptions
{
    public const string SectionName = "ConnectionStrings";
    public string ArithmeticDatabase { get; init; }
}