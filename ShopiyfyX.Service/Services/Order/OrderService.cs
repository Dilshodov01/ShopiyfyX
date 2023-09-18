using ShopiyfyX.Domain.Entities;
using System.Collections.Generic;
using ShopiyfyX.Data.Repositories;
using ShopiyfyX.Data.IRepositories;
using ShopiyfyX.Service.Exceptions;
using ShopiyfyX.Service.DTOs.OrderDto;
using ShopiyfyX.Service.DTOs.ProductDto;
using ShopiyfyX.Service.Interfaces.Order;

namespace ShopiyfyX.Service
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<User> userRepository = new Repository<User>();
        private readonly IRepository<Order> orderRepository = new Repository<Order>();

        public async Task<OrderForResultDto> CreateAsync(OrderForCreationDto dto)
        {
            // Check if an order with the same UserId exists
            var existingUser = await this.userRepository.SelectByIdAsync(dto.UserId);
            if (existingUser is null)
                throw new ShopifyXException(404, "User is not found.");


            // Order does not exist, create a new one
            var newOrder = new Order
            {
                UserId = dto.UserId,
                TotalAmount = dto.TotalAmount,
                CreatedAt = DateTime.UtcNow
            };

            var result = await this.orderRepository.InsertAsync(newOrder);

            return new OrderForResultDto()
            {
                Id = result.Id,
                UserId = result.UserId,
                TotalAmount = result.TotalAmount,
            };

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
                };
                result.Add(mappedOrder);
            }
            return result;
        }

        public async Task<OrderForResultDto> GetByIdAsync(long id)
        {
            var order = await this.orderRepository.SelectByIdAsync(id);

            if (order is null)
                throw new ShopifyXException(404, "Order is not found");

            return new OrderForResultDto
            {
                Id = order.Id,
                UserId = order.UserId,
                TotalAmount = order.TotalAmount,
            };
        }

        public async Task<bool> RemoveAsync(long id)
        {
            var order = await this.orderRepository.SelectByIdAsync(id);
            if (order is null)
                throw new ShopifyXException(404, "Order is not found.");

            return await this.orderRepository.DeleteAsync(id); ;
        }
    }
}
