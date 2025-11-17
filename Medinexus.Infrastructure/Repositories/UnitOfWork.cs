using Medinexus.Domain.Interfaces;
using Medinexus.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Storage;

namespace Medinexus.Infrastructure.Repositories
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IDbContextTransaction? _transaction;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }
        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            if (_transaction != null)
                await _transaction.CommitAsync();
        }

        public async Task RollbackAsync()
        {
            if (_transaction != null)
                await _transaction.RollbackAsync();
        }
    }
}
