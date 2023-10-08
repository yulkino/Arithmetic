namespace Domain.Entity;

public class User : IEntity, IEquatable<User>
{
    public User(string email, string identityId)
    {
        Id = Guid.NewGuid();
        Email = email;
        IdentityId = identityId;
    }

    public string Email { get; }
    public string IdentityId { get; }
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