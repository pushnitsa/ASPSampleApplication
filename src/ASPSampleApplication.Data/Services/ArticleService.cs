using ASPSampleApplication.Core.Models;
using ASPSampleApplication.Core.Services;
using ASPSampleApplication.Data.Models;
using ASPSampleApplication.Data.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace ASPSampleApplication.Data.Services
{
    public class ArticleService : IArticleService
    {
        private readonly Func<IEntryRepository> _entryRepositoryFactory;
        private readonly IMapper _mapper;

        public ArticleService(Func<IEntryRepository> entryRepositoryFactory, IMapper mapper)
        {
            _entryRepositoryFactory = entryRepositoryFactory;
            _mapper = mapper;
        }

        public async Task<Article> GetAsync(string id)
        {
            var result = await GetAsync(new[] { id });

            return result.FirstOrDefault();
        }

        public async Task<IReadOnlyCollection<Article>> GetAsync(IEnumerable<string> ids)
        {
            var result = new List<Article>();

            if (ids is not null && ids.Any())
            {
                await using var entityRepository = _entryRepositoryFactory();

                var entities = await entityRepository.Articles.Where(x => ids.Contains(x.Id)).ToListAsync();

                result.AddRange(_mapper.Map<IEnumerable<Article>>(entities));
            }

            return result.ToImmutableList();
        }

        public async Task SaveAsync(IEnumerable<Article> articles)
        {
            if (articles is null || !articles.Any())
            {
                return;
            }

            await using var entryRepository = _entryRepositoryFactory();

            var existingArticles = articles.Where(x => !string.IsNullOrEmpty(x.Id)).Select(x => x.Id).ToList();

            var existingEntries = await entryRepository.Articles.Where(x => existingArticles.Contains(x.Id)).ToListAsync();

            foreach (var article in articles)
            {
                var existsEntry = existingEntries.FirstOrDefault(x => x.Id == article.Id);

                if (existsEntry != null)
                {
                    // Update
                    existsEntry = _mapper.Map(article, existsEntry);
                }
                else
                {
                    // Create
                    var entity = _mapper.Map<ArticleEntity>(article);
                    entryRepository.Add(entity);
                }
            }

            await entryRepository.UnitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(IEnumerable<string> ids)
        {
            if (ids is not null && ids.Any())
            {
                await using var entryRepository = _entryRepositoryFactory();

                var existingEntries = await entryRepository.Articles.Where(x => ids.Contains(x.Id)).ToListAsync();

                foreach (var existingEntry in existingEntries)
                {
                    entryRepository.Delete(existingEntry);
                }

                await entryRepository.UnitOfWork.CommitAsync();
            }
        }
    }
}
