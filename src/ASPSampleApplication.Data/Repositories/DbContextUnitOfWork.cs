using ASPSampleApplication.Core.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ASPSampleApplication.Data.Repositories
{
    public class DbContextUnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;

        public DbContextUnitOfWork(DbContext context)
        {
            _dbContext = context;
        }

        public Task<int> CommitAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
    }
}
