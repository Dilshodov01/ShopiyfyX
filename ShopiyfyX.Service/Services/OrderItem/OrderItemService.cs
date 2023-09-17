using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopiyfyX.Data.IRepositories;
using ShopiyfyX.Data.Repositories;
using ShopiyfyX.Domain.Entities;
using ShopiyfyX.Service.DTOs.OrderItemDto;
using ShopiyfyX.Service.DTOs.ProductDto;
using ShopiyfyX.Service.Exceptions;
using ShopiyfyX.Service.Interfaces.OrderItem;

namespace ShopiyfyX.Service;

public class OrderItemService : IOrderItemService
{
    private readonly IRepository<OrderItem> orderItemRepository = new Repository<OrderItem>();
    public async Task<bool> RemoveAsync(long id)
    {
        var orderItem = await this.orderItemRepository.SelectByIdAsync(id);
        if (orderItem is null)
            throw new ShopifyXException(404, "Product is not found.");

        return await this.orderItemRepository.DeleteAsync(id); ;
    }

    public async Task<List<OrderItemForResultDto>> GetAllAsync()
    {
        var orderItems = await this.orderItemRepository.SelectAllAsync();
        var result = new List<OrderItemForResultDto>();

        foreach (var order in orderItems)
        {
            var mappedOrderItem = new OrderItemForResultDto()
            {
                Id = order.Id,
                OrderId = order.OrderId,
                ProductId = order.ProductId,
            };
            result.Add(mappedOrderItem);
        }

        return result;
    }

    public async Task<OrderItemForResultDto> GetByIdAsync(long id)
    {
        var orderItem = await this.orderItemRepository.SelectByIdAsync(id);
        if (orderItem is null)
            throw new ShopifyXException(404, "Product is not found.");

        return new OrderItemForResultDto()
        {
            Id = orderItem.Id,
            OrderId = orderItem.OrderId,
            ProductId = orderItem.ProductId,
        };
    }

    public async Task<OrderItemForResultDto> UpdateAsync(OrderItemForUpdateDto dto)
    {
        var user = await this.orderItemRepository.SelectByIdAsync(dto.Id);
        if (user is null)
            throw new ShopifyXException(404, "OrderItem is not found");

        var mappedProduct = new OrderItem()
        {
            Id = dto.Id,
            OrderId = dto.OrderId,
            ProductId = dto.ProductId,
            UpdatedAt = DateTime.UtcNow
        };

        await this.orderItemRepository.UpdateAsync(mappedProduct);

        return new OrderItemForResultDto()
        {
            Id = dto.Id,
            OrderId = dto.OrderId,
            ProductId = dto.ProductId,
        };
    }

    public async Task<OrderItemForResultDto> CreateAsync(OrderItemForCreationDto dto)
    {
        var existingOrderItem = (await this.orderItemRepository.SelectAllAsync()).FirstOrDefault(x => x.OrderId== dto.OrderId);
        if (existingOrderItem is null)
        {
            var orderItem = new OrderItem()
            {
                OrderId = dto.OrderId,
                ProductId = dto.ProductId,
                CreatedAt = DateTime.UtcNow
            };

            var result = await orderItemRepository.InsertAsync(orderItem);

            var mappedOrderItem = new OrderItemForResultDto()
            {
                Id = result.Id,
                OrderId = result.OrderId,
                ProductId = result.ProductId,
            };
            return mappedOrderItem;
        }
        else
            throw new ShopifyXException(400, "OrderItem is already exist");
    }
}
