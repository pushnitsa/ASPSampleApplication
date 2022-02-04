using ASPSampleApplication.Data.Models;

namespace ASPSampleApplication.Data.Repositories
{
    public interface IEntryRepository : IRepository
    {
        IQueryable<ArticleEntity> Articles { get; }
    }
}
