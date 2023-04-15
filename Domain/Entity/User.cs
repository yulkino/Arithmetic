namespace Domain.Entity;
public class User : IEntity
{
    public Guid Id { get; }
    public string Login { get; }
    public string PasswordHash { get; }

    public User(string login, string passwordHash)
    {
        Id = Guid.NewGuid();
        Login = login;
        PasswordHash = passwordHash;
    }
}