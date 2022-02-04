using ASPSampleApplication.Core.Models;

namespace ASPSampleApplication.Core.Services
{
    public interface IArticleSearchService
    {
        Task<ArticleSearchResult> SearchAsync(ArticleSearchCriteria searchCriteria);
    }
}
