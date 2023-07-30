namespace Domain.Entity;

public class User : IEntity, IEquatable<User>
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

    public bool Equals(User? other)
    {
        if (other is null)
            return false;

        return ReferenceEquals(this, other) || Id.Equals(other.Id);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        return obj.GetType() == GetType() && Equals((User)obj);
    }

    public override int GetHashCode() => Id.GetHashCode();
}