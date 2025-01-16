using ElasticSearch.Web.Services;
using ElasticSearch.Web.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ElasticSearch.Web.Controllers
{
    public class ECommerceController : Controller
    {
        private readonly ECommerceService _eCommerceService;

        public ECommerceController(ECommerceService eCommerceService)
        {
            _eCommerceService = eCommerceService;
        }

        public async Task<IActionResult> Search([FromQuery] SearchEcommerceDataViewModel searchEcommerceDataViewModel)
        {
            var (list, totalCount, pageLinkCount) = await _eCommerceService.EcommerceSearchAsync(searchEcommerceDataViewModel.SearchViewModel, searchEcommerceDataViewModel.Page, searchEcommerceDataViewModel.PageSize);

            searchEcommerceDataViewModel.TotalCount = totalCount;
            searchEcommerceDataViewModel.ECommerceList = list;
            searchEcommerceDataViewModel.PageLinkCount = pageLinkCount;

            return View(searchEcommerceDataViewModel);
        }
    }
}
