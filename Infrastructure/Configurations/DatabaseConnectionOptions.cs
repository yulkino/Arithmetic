namespace Infrastructure.Configurations;

public class DatabaseConnectionOptions
{
    public const string SectionName = "ConnectionStrings";
    public string ArithmeticDatabase { get; init; }
}