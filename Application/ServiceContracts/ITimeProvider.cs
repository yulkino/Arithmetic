namespace Application.ServiceContracts;

public interface ITimeProvider
{
    DateTime Now { get; }
}