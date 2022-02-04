using ASPSampleApplication.Core.Models;
using ASPSampleApplication.Core.Services;
using ASPSampleApplication.Data.Models;
using ASPSampleApplication.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using System.Linq.Dynamic.Core;

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
            var result = new ArticleSearchResult();

            if (searchCriteria is null)
            {
                return result;
            }

            await using var entryRespository = _repositoryFactory();

            var query = entryRespository.Articles;

            query = BuildSorting(query, searchCriteria.Sorting);

            var articleIds = await query
                .Skip(searchCriteria.Skip)
                .Take(searchCriteria.Take)
                .Select(x => x.Id)
                .ToListAsync();

            result.Results = (await _articleService.GetAsync(articleIds)).OrderBy(x => Array.IndexOf(articleIds.ToArray(), x.Id)).ToImmutableList();
            result.TotalCount = await entryRespository.Articles.CountAsync();

            return result;
        }

        protected virtual IQueryable<ArticleEntity> BuildSorting(IQueryable<ArticleEntity> query, string? sorting)
        {
            if (!string.IsNullOrEmpty(sorting))
            {
                query = query.OrderBy(sorting);
            }

            return query;
        }
    }
}
