using ASPSampleApplication.Core.Infrastructure;
using ASPSampleApplication.Core.Models;
using ASPSampleApplication.Data.Mapping;
using ASPSampleApplication.Data.Models;
using ASPSampleApplication.Data.Repositories;
using ASPSampleApplication.Data.Services;
using AutoFixture;
using AutoMapper;
using MockQueryable.Moq;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ASPSampleApplication.Tests
{
    public class ArticleServiceTests
    {
        private readonly Fixture _fixture = new();

        [Fact]
        public async Task TestGettingObjects()
        {
            // Arrange
            var repository = new Mock<IEntryRepository>();
            repository.Setup(x => x.UnitOfWork).Returns(new Mock<IUnitOfWork>().Object);

            var articles = _fixture.CreateMany<ArticleEntity>(1).ToList();
            var articleId = Guid.NewGuid().ToString();
            articles.ForEach(x => x.Id = articleId);

            repository.Setup(x => x.Articles).Returns(articles.AsQueryable().BuildMock().Object);

            var repositoryFactory = new Mock<Func<IEntryRepository>>();
            repositoryFactory.Setup(x => x.Invoke()).Returns(repository.Object);

            var mapper = new Mapper(new MapperConfiguration(x => x.AddProfile(new ArticleMappingProfile())));

            var articleService = new ArticleService(repositoryFactory.Object, mapper);

            // Act
            var article = await articleService.GetAsync(articleId);

            // Assert
            Assert.NotNull(article);
            Assert.Equal(articleId, article.Id);
            Assert.IsType<Article>(article);
            repository.Verify(x => x.Articles, Times.Once);
            repository.Verify(x => x.UnitOfWork, Times.Never);
        }
    }
}
