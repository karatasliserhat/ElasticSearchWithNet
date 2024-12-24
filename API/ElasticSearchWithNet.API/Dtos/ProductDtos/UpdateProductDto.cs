namespace ElasticSearchWithNet.API.Dtos.ProductDtos
{
    public record UpdateProductDto(string id, string Name, decimal Price, int Stock, ProductFeatureCommandDto? Feature)
    {
    }
}
