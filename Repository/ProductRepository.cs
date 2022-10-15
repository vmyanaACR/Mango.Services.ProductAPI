using AutoMapper;
using Mango.Services.ProductAPI.Data;
using Mango.Services.ProductAPI.Models;
using Mango.Services.ProductAPI.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ProductAPI.Repository;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly IMapper _mapper;

    public ProductRepository(ApplicationDbContext applicationDbContext, IMapper mapper)
    {
        _applicationDbContext = applicationDbContext;
        _mapper = mapper;
    }

    public async Task<ProductDto> CreateUpdateProduct(ProductDto productDto)
    {
        Product product = _mapper.Map<ProductDto, Product>(productDto);
        if (product.ProductId >0)
        {
            _applicationDbContext.Products.Update(product);
        }
        else
        {
            _applicationDbContext.Products.Add(product);
        }
        await _applicationDbContext.SaveChangesAsync();
        return _mapper.Map<Product, ProductDto>(product);
    }

    public async Task<bool> DeleteProduct(int productId)
    {
        try
        {
            Product product = await _applicationDbContext.Products.FirstOrDefaultAsync(x=>x.ProductId == productId);
            if (product == null)
            {
                return false;
            }
            _applicationDbContext.Products.Remove(product);
            await _applicationDbContext.SaveChangesAsync();
            return true;

        }
        catch(Exception ex)
        {
            return false;
        }
    }

    public async Task<ProductDto> GetProductById(int productId)
    {
        Product product = await _applicationDbContext.Products.Where(x=>x.ProductId == productId).FirstOrDefaultAsync();
        return _mapper.Map<ProductDto>(product);
    }

    public async Task<IEnumerable<ProductDto>> GetProducts()
    {
        List<Product> products = await _applicationDbContext.Products.ToListAsync();
        return _mapper.Map<List<ProductDto>>(products);
    }
}