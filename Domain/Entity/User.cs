namespace Domain.Entity;
public class User : IEntity
{
    public Guid Id { get; init; }
    public string Login { get; init; }

    public User(string login)
    {
        Id = Guid.NewGuid();
        Login = login;
    }
}