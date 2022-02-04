using ASPSampleApplication.Core.Models;
using ASPSampleApplication.Core.Services;
using ASPSampleApplication.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ASPSampleApplication.Data.Services
{
    public class ArticleSearchService : IArticleSearchService
    {
        private readonly Func<IEntryRepository> _repositoryFactory;
        private readonly IArticleService _articleService;

        public ArticleSearchService(Func<IEntryRepository> repositoryFactory, IArticleService articleService)
        {
            _repositoryFactory = repositoryFactory;
            _articleService = articleService;
        }

        public async Task<ArticleSearchResult> SearchAsync(ArticleSearchCriteria searchCriteria)
        {
            await using var entryRespository = _repositoryFactory();

            var articleIds = await entryRespository.Articles.Skip(searchCriteria.Skip).Take(searchCriteria.Take).Select(x => x.Id).ToListAsync();

            var result = new ArticleSearchResult
            {
                Results = await _articleService.GetAsync(articleIds),
                TotalCount = await entryRespository.Articles.CountAsync(),
            };

            return result;
        }
    }
}
