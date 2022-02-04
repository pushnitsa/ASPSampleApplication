namespace ASPSampleApplication.Core.Models
{
    public class ArticleSearchResult
    {
        public int TotalCount { get; set; }
        public IReadOnlyCollection<Article> Results { get; set; }
    }
}
