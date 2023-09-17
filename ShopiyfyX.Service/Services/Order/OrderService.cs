using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopiyfyX.Data.IRepositories;
using ShopiyfyX.Data.Repositories;
using ShopiyfyX.Domain.Entities;
using ShopiyfyX.Service.DTOs.OrderDto;
using ShopiyfyX.Service.DTOs.ProductDto;
using ShopiyfyX.Service.Exceptions;
using ShopiyfyX.Service.Interfaces.Order;

namespace ShopiyfyX.Service.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> orderRepository = new Repository<Order>();

        public async Task<OrderForResultDto> CreateAsync(OrderForCreationDto dto)
        {
            // Check if an order with the same UserId exists
            var existingOrder = (await orderRepository.SelectAllAsync())
                .FirstOrDefault(o => o.UserId == dto.UserId);

            if (existingOrder is null)
            {
                // Order does not exist, create a new one
                var newOrder = new Order
                {
                    UserId = dto.UserId,
                    TotalAmount = dto.TotalAmount,
                    Quantity = dto.Quantity,
                    CreatedAt = DateTime.UtcNow
                };

                var result = await orderRepository.InsertAsync(newOrder);

                return new OrderForResultDto()
                {
                    Id = result.Id,
                    UserId = result.UserId,
                    TotalAmount = result.TotalAmount,
                    Quantity = result.Quantity,
                };
            }

            else
            {
                // Order already exists, update its quantity
                existingOrder.Quantity += dto.Quantity;
                existingOrder.UpdatedAt = DateTime.UtcNow;

                await orderRepository.UpdateAsync(existingOrder);

                return new OrderForResultDto()
                {
                    Id = existingOrder.Id,
                    UserId = existingOrder.UserId,
                    TotalAmount = existingOrder.TotalAmount,
                    Quantity = existingOrder.Quantity,
                };
            }
        }

        public async Task<List<OrderForResultDto>> GetAllAsync()
        {
            var orders = await this.orderRepository.SelectAllAsync();
            var result = new List<OrderForResultDto>();

            foreach (var order in orders)
            {
                var mappedOrder = new OrderForResultDto()
                {
                    Id = order.Id,
                    UserId = order.UserId,
                    TotalAmount = order.TotalAmount,
                    Quantity = order.Quantity,
                };
                result.Add(mappedOrder);
            }
            return result;
        }

        public async Task<OrderForResultDto> GetByIdAsync(long id)
        {
            var order = await orderRepository.SelectByIdAsync(id);

            if (order is null)
                throw new ShopifyXException(404, "Order is not found");

            return new OrderForResultDto
            {
                Id = order.Id,
                UserId = order.UserId,
                TotalAmount = order.TotalAmount,
                Quantity = order.Quantity,
            };
        }

        public async Task<bool> RemoveAsync(long id)
        {
            var order = await this.orderRepository.SelectByIdAsync(id);
            if (order is null)
                throw new ShopifyXException(404, "Order is not found.");

            return await this.orderRepository.DeleteAsync(id); ;
        }

        public async Task<OrderForResultDto> UpdateAsync(OrderForUpdateDto dto)
        {
            var order = await this.orderRepository.SelectByIdAsync(dto.Id);
            if (order is null)
                throw new ShopifyXException(404, "Order is not found");

            var mappedOrder = new Order()
            {
                Id = dto.Id,
                UserId = dto.UserId,
                TotalAmount = dto.TotalAmount,
                Quantity = dto.Quantity,
                UpdatedAt = DateTime.UtcNow
            };

            await this.orderRepository.UpdateAsync(mappedOrder);

            var result = new OrderForResultDto()
            {
                Id = dto.Id,
                UserId = dto.UserId,
                TotalAmount = dto.TotalAmount,
                Quantity = dto.Quantity,
            };

            return result;
        }
    }
}
