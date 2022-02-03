using ASPSampleApplication.Core.Infrastructure;

namespace ASPSampleApplication.Data.Repositories
{
    public interface IRepository : IAsyncDisposable
    {
        IUnitOfWork UnitOfWork { get; }

        void Add<T>(T entity);
        void Update<T>(T entity);
        void Delete<T>(T entity);
    }
}
