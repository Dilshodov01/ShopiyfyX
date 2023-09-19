using ShopiyfyX.Domain.Entities;
using System.Collections.Generic;
using ShopiyfyX.Data.Repositories;
using ShopiyfyX.Data.IRepositories;
using ShopiyfyX.Service.Exceptions;
using ShopiyfyX.Service.DTOs.ProductDto;
using ShopiyfyX.Service.DTOs.OrderItemDto;
using ShopiyfyX.Service.Interfaces.OrderItem;
using System.Security.Cryptography;

namespace ShopiyfyX.Service;

public class OrderItemService : IOrderItemService
{
    private readonly IRepository<OrderItem> orderItemRepository = new Repository<OrderItem>();
    private readonly IRepository<Order> orderRepository = new Repository<Order>();
    private readonly IRepository<Product> productRepository = new Repository<Product>();
    private int _id;

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
        var existingOrder = await this.orderRepository.SelectByIdAsync(dto.OrderId);
        if (existingOrder is null)
            throw new ShopifyXException(404, "Order id is not found.");

        var existingProduct = await this.productRepository.SelectByIdAsync(dto.ProductId);
        if (existingProduct is null)
            throw new ShopifyXException(404, "Product id is not found.");

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
        var existingOrder = await this.orderRepository.SelectByIdAsync(dto.OrderId);
        if (existingOrder is null)
            throw new ShopifyXException(404, "Order id is not found.");

        var existingProduct = await this.productRepository.SelectByIdAsync(dto.ProductId);
        if (existingProduct is null)
            throw new ShopifyXException(404, "Product id is not found.");

        await GenerateIdAsync();
        var orderItem = new OrderItem()
        {
            Id = _id,
            OrderId = dto.OrderId,
            ProductId = dto.ProductId,
            CreatedAt = DateTime.UtcNow
        };

        var result = await orderItemRepository.InsertAsync(orderItem);

        var mappedOrderItem = new OrderItemForResultDto()
        {
            Id = _id,
            OrderId = result.OrderId,
            ProductId = result.ProductId,
        };
        return mappedOrderItem;
    }

    // Generation Id
    public async Task<long> GenerateIdAsync()
    {
        var orders = await orderItemRepository.SelectAllAsync();
        var count = orders.Count();
        if (count == 0)
        {
            _id = 1;
        }
        else
        {
            var order = orders[count - 1];
            _id = (int)(++order.Id);
        }
        return _id;
    }
}
