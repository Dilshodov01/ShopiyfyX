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

namespace ShopiyfyX.Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> productRepository = new Repository<Product>();

        public async Task<ProductForResultDto> CreateAsync(ProductForCreationDto dto)
        {
            var existingProduct = (await this.productRepository.SelectAllAsync()).FirstOrDefault(x => x.ProductName.ToLower() == dto.ProductName.ToLower());
            if (existingProduct is null)
            {
                var product = new Product()
                {
                    ProductName = dto.ProductName,
                    Price = dto.Price,
                    Description = dto.Description,
                    Quantity = dto.Quantity, // Set the initial quantity
                    CategoryId = dto.CategoryId,
                    CreatedAt = DateTime.UtcNow,
                };

                var result = await this.productRepository.InsertAsync(product);

                var mappedProduct = new ProductForResultDto()
                {
                    Id = result.Id,
                    ProductName = result.ProductName,
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
                await this.productRepository.UpdateAsync(existingProduct);

                var mappedProduct = new ProductForResultDto()
                {
                    Id = existingProduct.Id,
                    ProductName = existingProduct.ProductName,
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
                    ProductName = product.ProductName,
                    Price = product.Price,
                    Description = product.Description,
                    Quantity= product.Quantity,
                    CategoryId = product.CategoryId,
                };
                result.Add(mappedUser);
            }

            return result;
        }

        public async Task<ProductForResultDto> GetByIdAsync(long id)
        {
            var product = await this.productRepository.SelectByIdAsync(id);
            if (product is  null)
                throw new ShopifyXException(404,"Product is not found.");

            return new ProductForResultDto()
            {
                Id = product.Id,
                ProductName = product.ProductName,
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

            var mappedProduct = new Product()
            {
                Id = dto.Id,
                ProductName = dto.ProductName,
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
                ProductName = dto.ProductName,
                Price = dto.Price,
                Description = dto.Description,
                Quantity = dto.Quantity,
                CategoryId = dto.CategoryId,
            };

            return result;

        }
    }
}
