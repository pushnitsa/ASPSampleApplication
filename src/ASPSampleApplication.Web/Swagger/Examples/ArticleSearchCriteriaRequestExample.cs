using ASPSampleApplication.Core.Models;
using Swashbuckle.AspNetCore.Filters;

namespace ASPSampleApplication.Web.Swagger.Examples
{
    public class ArticleSearchCriteriaRequestExample : IExamplesProvider<ArticleSearchCriteria>
    {
        public ArticleSearchCriteria GetExamples()
        {
            return new ArticleSearchCriteria
            {
                Sorting = "CreatedDate DESC",
                Take = 20,
            };
        }
    }
}
