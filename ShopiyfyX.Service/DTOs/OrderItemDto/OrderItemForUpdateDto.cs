namespace ShopiyfyX.Service.DTOs.OrderItemDto;

public class OrderItemForUpdateDto
{
    public long Id { get; set; }
    public long OrderId { get; set; }
    public long ProductId { get; set; }
    public decimal Quantity { get; set; }
}
