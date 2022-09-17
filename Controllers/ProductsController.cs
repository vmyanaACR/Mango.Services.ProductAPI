using Mango.Services.ProductAPI.Data;
using Mango.Services.ProductAPI.Models.Dto;
using Mango.Services.ProductAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.ProductAPI.Controllers;

[Route("api/products")]
public class ProductsController : ControllerBase
{
    protected ResponseDto _responseDto;
    private readonly IProductRepository _productRepository;

    public ProductsController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
        _responseDto = new ResponseDto();
    }

    [HttpGet]
    public async Task<object> Get()
    {
        try
        {
            IEnumerable<ProductDto> products = await _productRepository.GetProducts();
            _responseDto.Result = products;
        }
        catch(Exception ex)
        {
            _responseDto.IsSuccess = false;
            _responseDto.ErrorMessages = new List<string> { ex.ToString() };
        }
        return _responseDto;
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<object> Get(int id)
    {
        try
        {
            ProductDto product = await _productRepository.GetProductById(id);
            _responseDto.Result = product;
        }
        catch(Exception ex)
        {
            _responseDto.IsSuccess = false;
            _responseDto.ErrorMessages = new List<string> { ex.ToString() };
        }
        return _responseDto;
    }

    [HttpPost]
    public async Task<object> Post([FromBody] ProductDto productDto)
    {
        try
        {
            ProductDto product = await _productRepository.CreateUpdateProduct(productDto);
            _responseDto.Result = product;
        }
        catch(Exception ex)
        {
            _responseDto.IsSuccess = false;
            _responseDto.ErrorMessages = new List<string> { ex.ToString() };
        }
        return _responseDto;
    }

    [HttpPut]
    public async Task<object> Put([FromBody] ProductDto productDto)
    {
        try
        {
            ProductDto product = await _productRepository.CreateUpdateProduct(productDto);
            _responseDto.Result = product;
        }
        catch(Exception ex)
        {
            _responseDto.IsSuccess = false;
            _responseDto.ErrorMessages = new List<string> { ex.ToString() };
        }
        return _responseDto;
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<object> Delete(int id)
    {
        try
        {
            bool IsSuccess = await _productRepository.DeleteProduct(id);
            _responseDto.Result = IsSuccess;
        }
        catch(Exception ex)
        {
            _responseDto.IsSuccess = false;
            _responseDto.ErrorMessages = new List<string> { ex.ToString() };
        }
        return _responseDto;
    }
}