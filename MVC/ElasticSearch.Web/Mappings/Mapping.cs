using AutoMapper;
using ElasticSearch.Web.Models;
using ElasticSearch.Web.ViewModel;

namespace ElasticSearch.Web.Mappings
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Blog, CreateBlogViewModel>()
                .AfterMap((x, y) => { x.Tags = y.Tags.ToString()!.Split(","); })
                .ReverseMap();
            CreateMap<Blog, UpdateBlogViewModel>().ReverseMap();
            CreateMap<Blog, ResultBlogViewModel>().ReverseMap();
        }
    }
}
