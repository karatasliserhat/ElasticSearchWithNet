using AutoMapper;
using ElasticSearchWithNet.API.Dtos.ProductDtos;
using ElasticSearchWithNet.API.Models;
using ElasticSearchWithNet.API.Repositories;
using System.Net;

namespace ElasticSearchWithNet.API.Services
{
    public class ProductService
    {
        private readonly ProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductService> _logger;
        public ProductService(ProductRepository repository, IMapper mapper, ILogger<ProductService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ResponseDto<ResponseProductDto>> SaveAsync(CreateProductDto createProductDto)
        {
            var dataMap = _mapper.Map<Product>(createProductDto);

            var response = await _repository.SaveAsync(dataMap);

            var responseProductDto = _mapper.Map<ResponseProductDto>(response);
            if (responseProductDto is null)
                return ResponseDto<ResponseProductDto>.Fail("Kayıt esnasında bir hata meydana geldi", HttpStatusCode.InternalServerError);
            return ResponseDto<ResponseProductDto>.Success(responseProductDto, HttpStatusCode.Created);

        }
        public async Task<ResponseDto<List<ResponseProductDto>>> GetAllProductAsync()
        {
            var response = await _repository.GetAllAsync();
            return ResponseDto<List<ResponseProductDto>>.Success(_mapper.Map<List<ResponseProductDto>>(response), HttpStatusCode.OK);
        }
        public async Task<ResponseDto<ResponseProductDto>> GetByIdAsync(string id)
        {
            var result = _mapper.Map<ResponseProductDto>(await _repository.GetByIdAsync(id));

            if (result is null) return ResponseDto<ResponseProductDto>.Fail("Ürün Bulunamaı", HttpStatusCode.NotFound);
            return ResponseDto<ResponseProductDto>.Success(result, HttpStatusCode.OK);
        }
    }
}
