namespace Domain.Entity;
public class User
{
    public int Id { get; private set; }
    public string Login { get; private set; }

    public User(int id, string login)
    {
        Id = id;
        Login = login;
    }
}