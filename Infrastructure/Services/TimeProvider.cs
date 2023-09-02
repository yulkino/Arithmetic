using Application.ServiceContracts;

namespace Infrastructure.Services;

internal class TimeProvider : ITimeProvider
{
    public DateTime Now => DateTime.Now;
}