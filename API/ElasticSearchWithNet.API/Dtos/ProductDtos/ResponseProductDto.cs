
namespace ElasticSearchWithNet.API.Dtos.ProductDtos
{
    public record ResponseProductDto(string Id, string Name, decimal Price, int Stock, ProductFeatureDto? Feature)
    {

    }
}
