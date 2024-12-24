using ElasticSearchWithNet.API.Models;

namespace ElasticSearchWithNet.API.Dtos.ProductDtos
{
    public record ProductFeatureCommandDto(int Width, int Height, EColor Color)
    {
    }
}
