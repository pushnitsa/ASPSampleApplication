using ASPSampleApplication.Data.Models;
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
            modelBuilder.Entity<ArticleEntity>().ToTable("Article").HasKey(x => x.Id);
            modelBuilder.Entity<ArticleEntity>().Property(x => x.Id).HasMaxLength(128).ValueGeneratedOnAdd();

            base.OnModelCreating(modelBuilder);
        }
    }
}
