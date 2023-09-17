using ShopiyfyX.Service.DTOs;

namespace ShopiyfyX.Service.Interfaces.Category;

public interface ICategoryService
{
    public Task<bool> RemoveAsync(long id);
    public Task<List<CategoryForResultDto>> GetAllAsync();
    public Task<CategoryForResultDto> GetByIdAsync(long id);
    public Task<CategoryForUpdateDto> UpdateAsync(CategoryForUpdateDto dto);
    public Task<CategoryForResultDto> CreateAsync(CategoryForCreationDto dto);
}
