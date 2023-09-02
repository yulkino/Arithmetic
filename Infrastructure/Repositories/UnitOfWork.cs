using Application.ServiceContracts;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

internal class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context) => _context = context;

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        _context.ChangeTracker.DetectChanges();
        return _context.SaveChangesAsync(cancellationToken);
    }
}