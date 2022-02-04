using ASPSampleApplication.Core.Models;

namespace ASPSampleApplication.Core.Services
{
    public interface IArticleService
    {
        Task<Article> GetAsync(string id);
        Task<IReadOnlyCollection<Article>> GetAsync(IEnumerable<string> ids);
        Task SaveAsync(IEnumerable<Article> articles);
        Task DeleteAsync(IEnumerable<string> ids);
    }
}
