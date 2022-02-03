namespace ASPSampleApplication.Core.Infrastructure
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();
    }
}
