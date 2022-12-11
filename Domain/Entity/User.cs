namespace Domain.Entity;
public class User : IEntity
{
    public Guid Id { get; init; }
    public string Login { get; init; }
    public string PasswordHash { get; init; }

    public User(string login, string passwordHash)
    {
        Id = Guid.NewGuid();
        Login = login;
        PasswordHash = passwordHash;
    }
}