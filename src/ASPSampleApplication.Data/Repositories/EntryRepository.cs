using ASPSampleApplication.Data.Models;

namespace ASPSampleApplication.Data.Repositories
{
    public class EntryRepository : Repository, IEntryRepository
    {
        private readonly EntryDbContext _dbContext;

        public EntryRepository(EntryDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<ArticleEntity> Articles { get => _dbContext.Set<ArticleEntity>(); }
    }
}
