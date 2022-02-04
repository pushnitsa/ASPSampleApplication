using ASPSampleApplication.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace ASPSampleApplication.Data.Models
{
    public class ArticleEntity : IAuditableEntity
    {
        public string Id { get; set; }

        [StringLength(1024)]
        public string Title { get; set; }

        [StringLength(10000)]
        public string Body { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
