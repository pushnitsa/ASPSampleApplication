using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ASPSampleApplication.Data.Repositories
{
    public class DesignTimeEntryDbContextFactory : IDesignTimeDbContextFactory<EntryDbContext>
    {
        public EntryDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EntryDbContext>();
            optionsBuilder.UseSqlServer("Server=(local);Database=SampleEntryApplication;Trusted_Connection=True");

            return new EntryDbContext(optionsBuilder.Options);
        }
    }
}
