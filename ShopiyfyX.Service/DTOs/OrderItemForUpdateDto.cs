namespace ShopiyfyX.Service.DTOs;

public class OrderItemForUpdateDto
{
    public long Id { get; set; }
    public long OrderId { get; set; }
    public long ProductId { get; set; }
}
