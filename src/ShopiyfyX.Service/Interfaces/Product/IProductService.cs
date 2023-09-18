using ShopiyfyX.Service.DTOs.ProductDto;

namespace ShopiyfyX.Service.Interfaces.Product;

public interface IProductService
{
    public Task<bool> RemoveAsync(long id);
    public Task<List<ProductForResultDto>> GetAllAsync();
    public Task<ProductForResultDto> GetByIdAsync(long id);
    public Task<ProductForResultDto> UpdateAsync(ProductForUpdateDto dto);
    public Task<ProductForResultDto> CreateAsync(ProductForCreationDto dto);
}
