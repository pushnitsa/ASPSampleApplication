using ASPSampleApplication.Core.Models;
using ASPSampleApplication.Data.Models;
using AutoMapper;

namespace ASPSampleApplication.Data.Mapping
{
    public class ArticleMappingProfile : Profile
    {
        public ArticleMappingProfile()
        {
            CreateMap<Article, ArticleEntity>()
                // Do not need to map these properties
                .ForMember(x => x.CreatedDate, y => y.Ignore())
                .ForMember(x => x.ModifiedDate, y => y.Ignore())
                .ForMember(x => x.Id, y => y.Ignore())
                .ReverseMap();
        }
    }
}
