using ElasticSearchWithNet.API.Dtos.ProductDtos;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ElasticSearchWithNet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        [NonAction]
        public IActionResult CreateActionResult<T>(ResponseDto<T> responseDto)
        {
            if (responseDto.Status == HttpStatusCode.NoContent) return new ObjectResult(null) { StatusCode = responseDto.Status.GetHashCode() };
            return new ObjectResult(responseDto) { StatusCode = responseDto.Status.GetHashCode() };
        }
    }
}