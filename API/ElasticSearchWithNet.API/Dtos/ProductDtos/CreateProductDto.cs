namespace ElasticSearchWithNet.API.Dtos.ProductDtos
{
    public record CreateProductDto(string Name, decimal Price, int Stock, ProductFeatureCommandDto? Feature)
    {
    }
}
