namespace ASPSampleApplication.Core.Models
{
    public interface IAuditableEntity
    {
        DateTime CreatedDate { get; set; }
        DateTime? ModifiedDate { get; set; }
    }
}
