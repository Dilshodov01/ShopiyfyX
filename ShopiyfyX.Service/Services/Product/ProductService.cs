using ShopiyfyX.Data.Repositories;
using ShopiyfyX.Service.DTOs.ProductDto;
using ShopiyfyX.Service.Interfaces.Product;

namespace ShopiyfyX.Service.Services.Product;

public class ProductService : IProductService
{
    private readonly IRepository<Product> productRepository = new Repository<Product>();
    
}
