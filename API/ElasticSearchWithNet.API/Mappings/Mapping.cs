using AutoMapper;
using ElasticSearchWithNet.API.Dtos.ProductDtos;
using ElasticSearchWithNet.API.Models;

namespace ElasticSearchWithNet.API.Mappings
{
    public class Mapping:Profile
    {
        public Mapping()
        {
            CreateMap<ProductFeature, ProductFeatureDto>().ReverseMap();
            CreateMap<ProductFeature, ProductFeatureCommandDto>().ReverseMap();
            CreateMap<Product, CreateProductDto>().ForMember(x => x.Feature, opt => opt.MapFrom(x => x.Feature)).ReverseMap();
            CreateMap<Product, UpdateProductDto>().ForMember(x => x.Feature, opt => opt.MapFrom(x => x.Feature)).ReverseMap();
            CreateMap<Product, ResponseProductDto>().ForMember(x => x.Feature, opt => opt.MapFrom(x => x.Feature)).ReverseMap();
        }
    }
}
