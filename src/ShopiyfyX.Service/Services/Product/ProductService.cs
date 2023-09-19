using ShopiyfyX.Domain.Entities;

using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using ShopiyfyX.Data.Repositories;
using ShopiyfyX.Data.IRepositories;
using ShopiyfyX.Service.Exceptions;
using ShopiyfyX.Service.DTOs.UserDto;
using ShopiyfyX.Service.DTOs.ProductDto;
using ShopiyfyX.Service.Interfaces.Product;
using System.Security.Cryptography;
using ShopiyfyX.Service.DTOs.CategoryDto;

namespace ShopiyfyX.Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> productRepository = new Repository<Product>();
        private readonly IRepository<Category> categoryRepository = new Repository<Category>();
        private int _id;

        public async Task<ProductForResultDto> CreateAsync(ProductForCreationDto dto)
        {
            var existingProduct = (await this.productRepository.SelectAllAsync())
                .FirstOrDefault(x => x.Name.ToLower() == dto.Name.ToLower());
            if (existingProduct is null)
            {
                var findCategory = (await this.categoryRepository.SelectByIdAsync(dto.CategoryId));
                if (findCategory is null)
                    throw new ShopifyXException(404, "Product's category is not found.");

                await GenerateIdAsync();
                var product = new Product()
                {
                    Id = _id,
                    Name = dto.Name,
                    Price = dto.Price,
                    Description = dto.Description,
                    Quantity = dto.Quantity, // Set the initial quantity
                    CategoryId = dto.CategoryId,
                    CreatedAt = DateTime.UtcNow,
                };

                var result = await this.productRepository.InsertAsync(product);

                var mappedProduct = new ProductForResultDto()
                {
                    Id = _id,
                    Name = result.Name,
                    Price = result.Price,
                    Description = result.Description,
                    Quantity = result.Quantity,
                    CategoryId = result.CategoryId,
                };
                return mappedProduct;
            }
            else
            {
                // Update the quantity if the product already exists
                existingProduct.Quantity += dto.Quantity;
                existingProduct.UpdatedAt = DateTime.UtcNow;
                await this.productRepository.UpdateAsync(existingProduct);

                var mappedProduct = new ProductForResultDto()
                {
                    Id = existingProduct.Id,
                    Name = existingProduct.Name,
                    Price = existingProduct.Price,
                    Description = existingProduct.Description,
                    Quantity = existingProduct.Quantity,
                    CategoryId = existingProduct.CategoryId,
                };
                return mappedProduct;
            }
        }

        public async Task<List<ProductForResultDto>> GetAllAsync()
        {
            var products = await this.productRepository.SelectAllAsync();
            var result = new List<ProductForResultDto>();

            foreach (var product in products)
            {
                var mappedUser = new ProductForResultDto()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Description = product.Description,
                    Quantity = product.Quantity,
                    CategoryId = product.CategoryId,
                };
                result.Add(mappedUser);
            }

            return result;
        }

        public async Task<ProductForResultDto> GetByIdAsync(long id)
        {
            var product = await this.productRepository.SelectByIdAsync(id);
            if (product is null)
                throw new ShopifyXException(404, "Product is not found.");

            return new ProductForResultDto()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Quantity = product.Quantity,
                CategoryId = product.CategoryId,
            };
        }

        public async Task<bool> RemoveAsync(long id)
        {
            var product = await this.productRepository.SelectByIdAsync(id);
            if (product is null)
                throw new ShopifyXException(404, "Product is not found.");

            return await this.productRepository.DeleteAsync(id);
        }

        public async Task<ProductForResultDto> UpdateAsync(ProductForUpdateDto dto)
        {
            var user = await this.productRepository.SelectByIdAsync(dto.Id);
            if (user is null)
                throw new ShopifyXException(404, "Product is not found");

            // Check for category
            var existingCategory = await this.categoryRepository.SelectByIdAsync(dto.CategoryId);
            if (existingCategory is null)
                throw new ShopifyXException(404, "Category is not found");

            var mappedProduct = new Product()
            {
                Id = dto.Id,
                Name = dto.Name,
                Price = dto.Price,
                Description = dto.Description,
                Quantity = dto.Quantity,
                CategoryId = dto.CategoryId,
                UpdatedAt = DateTime.UtcNow
            };

            await this.productRepository.UpdateAsync(mappedProduct);

            var result = new ProductForResultDto()
            {
                Id = dto.Id,
                Name = dto.Name,
                Price = dto.Price,
                Description = dto.Description,
                Quantity = dto.Quantity,
                CategoryId = dto.CategoryId,
            };

            return result;

        }


        // Generation Id
        public async Task<long> GenerateIdAsync()
        {
            var products = await productRepository.SelectAllAsync();
            var count = products.Count();
            if (count == 0)
            {
                _id = 1;
            }
            else
            {
                var product = products[count - 1];
                _id = (int)(++product.Id);
            }
            return _id;
        }
    }
}
