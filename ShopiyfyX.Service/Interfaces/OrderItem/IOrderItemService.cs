using ShopiyfyX.Service.DTOs.OrderItemDto;

namespace ShopiyfyX.Service.Interfaces.OrderItem;

public interface IOrderItemService
{
    public Task<bool> RemoveAsync(long id);
    public Task<List<OrderItemForResultDto>> GetAllAsync();
    public Task<OrderItemForResultDto> GetByIdAsync(long id);
    public Task<OrderItemForUpdateDto> UpdateAsync(OrderItemForUpdateDto dto);
    public Task<OrderItemForResultDto> CreateAsync(OrderItemForCreationDto dto);
}
