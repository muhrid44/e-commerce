using Microsoft.EntityFrameworkCore.Storage;

namespace Identity.Repository.Interfaces
{
    public interface IDbTransactionRepository
    {
        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken);
    }
}
