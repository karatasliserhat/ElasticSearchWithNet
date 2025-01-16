using AutoMapper;
using ElasticSearch.Web.Repositories;
using ElasticSearch.Web.ViewModel;

namespace ElasticSearch.Web.Services
{
    public class ECommerceService
    {
        private readonly ECommerceRepository _repository;
        private readonly IMapper _mapper;

        public ECommerceService(ECommerceRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<(List<ECommerceViewModel>, long totalCount, long pageLinkCount)> EcommerceSearchAsync(EcommerceSearchViewModel ecommerceSearchView, int page, int pageSize)
        {

            var (eCommerceList, totalCount) = await _repository.SearchECommerceAsync(ecommerceSearchView, page, pageSize);

            var PageLinkCountCalculate = totalCount % pageSize;
            long PageLinkCount = 0;
            if (PageLinkCountCalculate == 0)
                PageLinkCount = totalCount / pageSize;
            else
                PageLinkCount = (totalCount / pageSize) + 1;

            var dataMap = _mapper.Map<List<ECommerceViewModel>>(eCommerceList);

            return (dataMap, totalCount, PageLinkCount);
        }
    }
}
