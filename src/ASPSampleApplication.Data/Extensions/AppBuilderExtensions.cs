using ASPSampleApplication.Core.Models;
using EntityFrameworkCore.Triggers;
using Microsoft.AspNetCore.Builder;

namespace ASPSampleApplication.Data.Extensions
{
    public static class AppBuilderExtensions
    {
        public static void UseDbTriggers(this IApplicationBuilder appBuilder)
        {
            Triggers<IAuditableEntity>.Inserting += entry =>
            {
                entry.Entity.CreatedDate = DateTime.UtcNow;
            };

            Triggers<IAuditableEntity>.Updating += entry =>
            {
                entry.Entity.ModifiedDate = DateTime.UtcNow;
            };
        }
    }
}
