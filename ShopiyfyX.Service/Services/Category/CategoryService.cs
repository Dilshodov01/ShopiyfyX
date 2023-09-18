using ShopiyfyX.Domain.Entities;
using ShopiyfyX.Data.Repositories;
using ShopiyfyX.Service.Exceptions;
using ShopiyfyX.Data.IRepositories;
using ShopiyfyX.Service.DTOs.CategoryDto;
using ShopiyfyX.Service.Interfaces.Category;

namespace ShopiyfyX.Service.Services;

public class CategoryService : ICategoryService
{
    private readonly IRepository<Category> category = new Repository<Category>();
 
    public async Task<CategoryForResultDto> CreateAsync(CategoryForCreationDto dto)
    {
        var data = (await this.category.SelectAllAsync()).FirstOrDefault(x => x.CategoryName == dto.CategoryName);
        if (data is not null)
            throw new ShopifyXException(404, "Category is not found.");

        var mappingdata = new Category()
        {
            CategoryName = dto.CategoryName,
        };

        var result = await this.category.InsertAsync(mappingdata);

        var ResultMapping = new CategoryForResultDto()
        {
            Id = result.Id,
            CategoryName = result.CategoryName
        };

        return ResultMapping;
        
    }

    public async Task<List<CategoryForResultDto>> GetAllAsync()
    {
        var result = await this.category.SelectAllAsync();
        var ls = new List<CategoryForResultDto>();
        foreach(var res in result)
        {
            var MappingResult = new CategoryForResultDto()
            {
                Id = res.Id,
                CategoryName = res.CategoryName,
            };
            ls.Add(MappingResult);
        }

        return ls;
        
    }

    public async Task<CategoryForResultDto> GetByIdAsync(long id)
    {
        var  data =await this.category.SelectByIdAsync(id);
        if (data is null)
            throw new ShopifyXException(404, "Category is not found");

        var mapping = new CategoryForResultDto()
        {
            Id = data.Id,
            CategoryName = data.CategoryName,
        };

        return mapping;

    }

    public async Task<bool> RemoveAsync(long id)
    {
        var data = await this.category.SelectByIdAsync(id);
        if(data == null)
            throw new ShopifyXException(404, "Category is not found.");

        var result = await this.category.DeleteAsync(id);
        return result;
    }


    public  async Task<CategoryForResultDto> UpdateAsync(CategoryForUpdateDto dto)
    {
        var data = await this.category.SelectByIdAsync(dto.Id);
        if(data == null)
            throw new ShopifyXException(404, "Category is not found.");

        var result = new Category()
        {
            Id = dto.Id,
            CategoryName = dto.CategoryName,
        };

        await this.category.UpdateAsync(result);

        var mapping = new CategoryForResultDto()
        {
            Id = data.Id,
            CategoryName = data.CategoryName,
        };

        return mapping;
    }
}
