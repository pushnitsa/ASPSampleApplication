using ASPSampleApplication.Core.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ASPSampleApplication.Data.Repositories
{
    public class Repository : IRepository
    {
        private readonly DbContext _dbContext;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
            UnitOfWork = new DbContextUnitOfWork(dbContext);
        }

        public IUnitOfWork UnitOfWork { get; }

        public void Add<T>(T entity)
        {
            _dbContext.Add(entity);
        }

        public void Update<T>(T entity)
        {
            _dbContext.Update(entity);
        }

        public void Delete<T>(T entity)
        {
            _dbContext.Remove(entity);
        }

        public async ValueTask DisposeAsync()
        {
            await DisposeAsync(true);
            GC.SuppressFinalize(this);
        }

        protected virtual ValueTask DisposeAsync(bool disposing)
        {
            return _dbContext.DisposeAsync();
        }
    }
}
