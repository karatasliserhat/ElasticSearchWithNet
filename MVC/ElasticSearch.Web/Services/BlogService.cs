using AutoMapper;
using ElasticSearch.Web.Models;
using ElasticSearch.Web.Repositories;
using ElasticSearch.Web.ViewModel;

namespace ElasticSearch.Web.Services
{
    public class BlogService
    {
        private readonly BlogRepository _blogRepository;
        private readonly IMapper _mapper;
        public BlogService(BlogRepository blogRepository, IMapper mapper)
        {
            _blogRepository = blogRepository;
            _mapper = mapper;
        }

        public async Task<bool> SaveAsync(CreateBlogViewModel model)
        {
            var isCreated = await _blogRepository.Save(_mapper.Map<Blog>(model));
            return isCreated != null;
        }
    }
}
