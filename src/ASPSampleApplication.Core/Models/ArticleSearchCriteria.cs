namespace ASPSampleApplication.Core.Models
{
    public class ArticleSearchCriteria
    {
        public int Take { get; set; } = 20;
        public int Skip { get; set; } = 0;
        public string? Sorting { get; set; }
    }
}
