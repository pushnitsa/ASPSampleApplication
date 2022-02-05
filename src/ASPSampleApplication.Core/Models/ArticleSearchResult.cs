namespace ASPSampleApplication.Core.Models
{
    public class ArticleSearchResult
    {
        public ArticleSearchResult()
        {
            Results = new List<Article>();
        }

        public int TotalCount { get; set; }
        public List<Article> Results { get; set; }
    }
}
