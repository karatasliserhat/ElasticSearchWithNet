using ElasticSearchWithNet.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace ElasticSearchWithNet.API.Controllers
{

    public class ECommercesController : BaseController
    {
        private readonly ECommerceService _service;

        public ECommercesController(ECommerceService service)
        {
            _service = service;
        }

        [HttpGet("{customerFirstName}")]
        public async Task<IActionResult> TermQuery(string customerFirstName)
        {
            return CreateActionResult(await _service.TermQueryAsync(customerFirstName));
        }
        [HttpPost]
        public async Task<IActionResult> TermsQuery(List<string> customerFirstNames)
        {
            return CreateActionResult(await _service.TermsQueryAsync(customerFirstNames));
        }
        [HttpGet("{customerFullName}")]
        public async Task<IActionResult> PrefixQuery(string customerFullName)
        {
            return CreateActionResult(await _service.PrefixQueryAsync(customerFullName));
        }
        [HttpGet("{fromPrice}/{toPrice}")]
        public async Task<IActionResult> RangeQuery(double fromPrice, double toPrice)
        {
            return CreateActionResult(await _service.RangeQueryAsync(fromPrice, toPrice));
        }
        [HttpGet]
        public async Task<IActionResult> MatchAllQuery()
        {
            return CreateActionResult(await _service.MatchAllQueryAsync());
        }
        [HttpGet("{page}/{pageSize}")]
        public async Task<IActionResult> PaginationQuery(int page=1, int pageSize=3)
        {
            return CreateActionResult(await _service.PaginationQueryAsync(page, pageSize));
        }
        [HttpGet("{customerFullName}")]
        public async Task<IActionResult> WildcardQuery(string customerFullName)
        {
            return CreateActionResult(await _service.WildcardQueryAsync(customerFullName));
        }
        [HttpGet("{customerFullName}")]
        public async Task<IActionResult> FuzzyQuery(string customerFullName)
        {
            return CreateActionResult(await _service.FuzzyQueryAsync(customerFullName));
        }
        [HttpGet("{category}")]
        public async Task<IActionResult> MatchQueryTextFull(string category)
        {
            return CreateActionResult(await _service.MatchQueryTextFullAsync(category));
        }

        [HttpGet("{customerFullName}")]
        public async Task<IActionResult> MatchBoolPrefixQueryTextFull(string customerFullName)
        {
            return CreateActionResult(await _service.MatchBoolPrefixQueryTextFullAsync(customerFullName));
        }
        
        [HttpGet("{customerFullName}")]
        public async Task<IActionResult> MatchPhraseQueryTextFull(string customerFullName)
        {
            return CreateActionResult(await _service.MatchPhraseQueryTextFullAsync(customerFullName));
        }

        
    }
}
