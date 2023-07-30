using Application.Services;

namespace Infrastructure.Services;

public class TimeProvider : ITimeProvider
{
    public DateTime Now => DateTime.Now;
}