using ShopiyfyX.Domain.Entities;
using ShopiyfyX.Data.Repositories;
using ShopiyfyX.Data.IRepositories;
using ShopiyfyX.Service.DTOs.ProductDto;
using ShopiyfyX.Service.Interfaces.Product;
using ShopiyfyX.Service.Exceptions;

namespace ShopiyfyX.Service.Services;

public class ProductService : IProductService
{
    private readonly IRepository<Product> productRepository = new Repository<Product>();

    public async Task<ProductForResultDto> CreateAsync(ProductForCreationDto dto)
    {
        var product = (await this.productRepository.SelectAllAsync()).FirstOrDefault(x => x.ProductName.ToLower() == dto.ProductName.ToLower());
        if (product is null)
        {
            var products = new Product()
            {
                ProductName = dto.ProductName,
                Price = dto.Price,
                Description = dto.Description,
                CreatedAt = DateTime.UtcNow
            };

            var result = await productRepository.InsertAsync(product);

            var mappedProduct = new ProductForResultDto()
            {
                Id = result.Id,
                ProductName = result.ProductName,
                Price = result.Price,
                Description = result.Description,
            };
            return mappedProduct;
        }

        throw new ShopifyXException(400, "exit");
    }

    public Task<List<ProductForResultDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ProductForResultDto> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RemoveAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<ProductForUpdateDto> UpdateAsync(ProductForUpdateDto dto)
    {
        throw new NotImplementedException();
    }
}
