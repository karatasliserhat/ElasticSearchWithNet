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
            return CreateActionResult(await _service.TermQuery(customerFirstName));
        }
        [HttpPost]
        public async Task<IActionResult> TermQuery(List<string> customerFirstNames)
        {
            return CreateActionResult(await _service.TermsQuery(customerFirstNames));
        }
        [HttpGet("{customerFullName}")]
        public async Task<IActionResult> PrefixQuery(string customerFullName)
        {
            return CreateActionResult(await _service.PrefixQuery(customerFullName));
        }
        [HttpGet("{fromPrice}/{toPrice}")]
        public async Task<IActionResult> RangeQuery(double fromPrice, double toPrice)
        {
            return CreateActionResult(await _service.RangeQuery(fromPrice, toPrice));
        }
        [HttpGet]
        public async Task<IActionResult> MatchAllQuery()
        {
            return CreateActionResult(await _service.MatchAllQuery());
        }
    }
}
