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

        public async Task<ResponseDto<List<ECommerce>>> TermQueryAsync(string customerFullName)
        {
            var result = await _repository.TermQueryAsync(customerFullName);

            return ResponseDto<List<ECommerce>>.Success(result.ToList(), HttpStatusCode.OK);
        }
        public async Task<ResponseDto<List<ECommerce>>> TermsQueryAsync(List<string> customerFirstNames)
        {
            var result = await _repository.TermsQueryAsync(customerFirstNames);
            return ResponseDto<List<ECommerce>>.Success(result.ToList(), HttpStatusCode.OK);

        }
        public async Task<ResponseDto<List<ECommerce>>> PrefixQueryAsync(string customerFullName)
        {
            var result = await _repository.PrefixQueryAsync(customerFullName);
            return ResponseDto<List<ECommerce>>.Success(result.ToList(), HttpStatusCode.OK);
        }
        public async Task<ResponseDto<List<ECommerce>>> RangeQueryAsync(double fromPrice, double toPrice)
        {
            var result = await _repository.RangeQueryAsync(fromPrice,toPrice);
            return ResponseDto<List<ECommerce>>.Success(result.ToList(), HttpStatusCode.OK);
        }
        public async Task<ResponseDto<List<ECommerce>>> MatchAllQueryAsync()
        {
            var result = await _repository.MatchAllQueryAsync();
            return ResponseDto<List<ECommerce>>.Success(result.ToList(), HttpStatusCode.OK);
        }
        public async Task<ResponseDto<List<ECommerce>>> PaginationQueryAsync(int page, int pageSize)
        {
            var result = await _repository.PaginationQueryAsync(page,pageSize);
            return ResponseDto<List<ECommerce>>.Success(result.ToList(), HttpStatusCode.OK);
        }
        public async Task<ResponseDto<List<ECommerce>>> WildcardQueryAsync(string customerFullName)
        {
            var result = await _repository.WildcardQueryAsync(customerFullName);
            return ResponseDto<List<ECommerce>>.Success(result.ToList(), HttpStatusCode.OK);
        }
        public async Task<ResponseDto<List<ECommerce>>> FuzzyQueryAsync(string customerFullName)
        {
            var result = await _repository.FuzzyQueryAsync(customerFullName);
            return ResponseDto<List<ECommerce>>.Success(result.ToList(), HttpStatusCode.OK);

        }
        public async Task<ResponseDto<List<ECommerce>>> MatchQueryTextFullAsync(string category)
        {
            var result = await _repository.MatchQueryTextFullAsync(category);
            return ResponseDto<List<ECommerce>>.Success(result.ToList(), HttpStatusCode.OK);
        }
        public async Task<ResponseDto<List<ECommerce>>> MatchBoolPrefixQueryTextFullAsync(string customerFullName)
        {
            var result = await _repository.MatchBoolPrefixQueryTextFullAsync(customerFullName);
            return ResponseDto<List<ECommerce>>.Success(result.ToList(), HttpStatusCode.OK);
        }
        public async Task<ResponseDto<List<ECommerce>>> MatchPhraseQueryTextFullAsync(string customerFullName)
        {
            var result = await _repository.MatchPhraseQueryTextFullAsync(customerFullName);
            return ResponseDto<List<ECommerce>>.Success(result.ToList(), HttpStatusCode.OK);
        }
    }
}
