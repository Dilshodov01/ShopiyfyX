namespace ShopiyfyX.Service.DTOs.OrderItemDto;

public class OrderItemForCreationDto
{
    public long OrderId { get; set; }
    public long ProductId { get; set; }
    public decimal Quantity { get; set; }
}
