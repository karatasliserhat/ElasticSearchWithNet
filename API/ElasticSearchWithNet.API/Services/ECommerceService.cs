using ElasticSearchWithNet.API.Dtos.ProductDtos;
using ElasticSearchWithNet.API.EcommerceModels;
using ElasticSearchWithNet.API.Repositories;
using System.Collections.Immutable;
using System.Net;

namespace ElasticSearchWithNet.API.Services
{
    public class ECommerceService
    {
        private readonly ECommerceRepository _repository;

        public ECommerceService(ECommerceRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResponseDto<List<ECommerce>>> TermQuery(string customerFullName)
        {
            var result = await _repository.TermQuery(customerFullName);

            return ResponseDto<List<ECommerce>>.Success(result.ToList(), HttpStatusCode.OK);
        }
        public async Task<ResponseDto<List<ECommerce>>> TermsQuery(List<string> customerFirstNames)
        {
            var result = await _repository.TermsQuery(customerFirstNames);
            return ResponseDto<List<ECommerce>>.Success(result.ToList(), HttpStatusCode.OK);

        }
        public async Task<ResponseDto<List<ECommerce>>> PrefixQuery(string customerFullName)
        {
            var result = await _repository.PrefixQuery(customerFullName);
            return ResponseDto<List<ECommerce>>.Success(result.ToList(), HttpStatusCode.OK);
        }
        public async Task<ResponseDto<List<ECommerce>>> RangeQuery(double fromPrice, double toPrice)
        {
            var result = await _repository.RangeQuery(fromPrice,toPrice);
            return ResponseDto<List<ECommerce>>.Success(result.ToList(), HttpStatusCode.OK);
        }
        public async Task<ResponseDto<List<ECommerce>>> MatchAllQuery()
        {
            var result = await _repository.MatchAllQuery();
            return ResponseDto<List<ECommerce>>.Success(result.ToList(), HttpStatusCode.OK);
        }
    }
}
