using ShopiyfyX.Service.DTOs.ProductDto;
using ShopiyfyX.Service.Interfaces.Product;
using ShopiyfyX.Data.IRepositories;
using ShopiyfyX.Data.Repositories;

namespace ShopiyfyX.Service.Services.Product;

public class ProductService : IProductService
{
    private readonly IRepository<Product> productRepository = new Repository<Product>();
    
}
