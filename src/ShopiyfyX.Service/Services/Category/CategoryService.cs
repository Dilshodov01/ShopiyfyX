using ShopiyfyX.Domain.Entities;
using ShopiyfyX.Data.Repositories;
using ShopiyfyX.Service.Exceptions;
using ShopiyfyX.Data.IRepositories;
using ShopiyfyX.Service.DTOs.CategoryDto;
using ShopiyfyX.Service.Interfaces.Category;
using System.Security.Cryptography;

namespace ShopiyfyX.Service.Services;

public class CategoryService : ICategoryService
{
    private readonly IRepository<Category> category = new Repository<Category>();
    private readonly IRepository<Product> productRepository = new Repository<Product>();
    private int _id;

    public async Task<CategoryForResultDto> CreateAsync(CategoryForCreationDto dto)
    {
        var category = (await this.category.SelectAllAsync())
            .FirstOrDefault(x => x.Name == dto.Name);
        if (category is not null)
            throw new ShopifyXException(404, "Category is not found.");

        await GenerateIdAsync();
        var mappedCategory = new Category()
        {
            Id = _id,
            Name = dto.Name,
            CreatedAt = DateTime.UtcNow,
        };

        var result = await this.category.InsertAsync(mappedCategory);

        var resultCategory = new CategoryForResultDto()
        {
            Id = _id,
            Name = result.Name
        };

        return resultCategory;
        
    }

    public async Task<List<CategoryForResultDto>> GetAllAsync()
    {
        var categories = await this.category.SelectAllAsync();
        var result = new List<CategoryForResultDto>();
        foreach(var category in categories)
        {
            var meppedCategory = new CategoryForResultDto()
            {
                Id = category.Id,
                Name = category.Name,
            };
            result.Add(meppedCategory);
        }

        return result;
        
    }

    public async Task<CategoryForResultDto> GetByIdAsync(long id)
    {
        var category = await this.category.SelectByIdAsync(id);
        if (category is null)
            throw new ShopifyXException(404, "Category is not found");

        var mappedResult = new CategoryForResultDto()
        {
            Id = category.Id,
            Name = category.Name,
        };

        return mappedResult;

    }

    public async Task<bool> RemoveAsync(long id)
    {
        var dataCategory = await this.category.SelectByIdAsync(id);
        if(dataCategory == null)
            throw new ShopifyXException(404, "Category is not found.");

        var products = await this.productRepository.SelectAllAsync();
        foreach(var product in products)
        {
            if(product.CategoryId == dataCategory.Id)
                await productRepository.DeleteAsync(product.Id);
        }

        return await this.category.DeleteAsync(id);
    }


    public  async Task<CategoryForResultDto> UpdateAsync(CategoryForUpdateDto dto)
    {
        var dataCategory = await this.category.SelectByIdAsync(dto.Id);
        if(dataCategory == null)
            throw new ShopifyXException(404, "Category is not found.");

        var result = new Category()
        {
            Id = dto.Id,
            Name = dto.Name,
            UpdatedAt = DateTime.UtcNow
        };

        await this.category.UpdateAsync(result);

        var mappedCategory = new CategoryForResultDto()
        {
            Id = dataCategory.Id,
            Name = dataCategory.Name,
        };

        return mappedCategory;
    }

    // Generation Id
    public async Task<long> GenerateIdAsync()
    {
        var categories = await category.SelectAllAsync();
        var count = categories.Count();
        if (count == 0)
        {
            _id = 1;
        }
        else
        {
            var category = categories[count - 1];
            _id = (int)(++category.Id);
        }
        return _id;
    }
}
