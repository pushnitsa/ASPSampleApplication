using EntityFrameworkCore.Triggers;
using Microsoft.EntityFrameworkCore;

namespace ASPSampleApplication.Data.Repositories
{
    public class EntryDbContext : DbContextWithTriggers
    {
        public EntryDbContext(DbContextOptions<EntryDbContext> options)
            : base(options)
        {

        }

        protected EntryDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
