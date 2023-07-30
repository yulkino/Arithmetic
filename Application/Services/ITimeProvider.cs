namespace Application.Services;

public interface ITimeProvider
{
    DateTime Now { get; }
}