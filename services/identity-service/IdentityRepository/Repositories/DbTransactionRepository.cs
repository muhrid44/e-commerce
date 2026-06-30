using Identity.Repository.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace Identity.Repository.Repositories
{
    public class DbTransactionRepository : IDbTransactionRepository
    {
        private readonly AppIdentityDbContext _context;

        public DbTransactionRepository(AppIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
        {
            return await _context.Database.BeginTransactionAsync(cancellationToken);
        }
    }
}
