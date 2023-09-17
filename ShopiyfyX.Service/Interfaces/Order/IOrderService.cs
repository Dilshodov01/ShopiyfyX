﻿using ShopiyfyX.Service.DTOs;

namespace ShopiyfyX.Service.Interfaces.Order;

public interface IOrderService
{
    public Task<bool> RemoveAsync(long id);
    public Task<List<OrderForResultDto>> GetAllAsync();
    public Task<OrderForResultDto> GetByIdAsync(long id);
    public Task<OrderForUpdateDto> UpdateAsync(OrderForUpdateDto dto);
    public Task<OrderForResultDto> CreateAsync(OrderForCreationDto dto);
}