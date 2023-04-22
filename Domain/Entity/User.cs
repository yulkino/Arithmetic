namespace Domain.Entity;

public class User : IEntity
{
    public User(string login, string passwordHash)
    {
        Id = Guid.NewGuid();
        Login = login;
        PasswordHash = passwordHash;
    }

    public string Login { get; }
    public string PasswordHash { get; }
    public Guid Id { get; }
}