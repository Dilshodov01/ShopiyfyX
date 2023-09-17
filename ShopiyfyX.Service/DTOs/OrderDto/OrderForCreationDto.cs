namespace ShopiyfyX.Service.DTOs.OrderDto;

public class OrderForCreationDto
{
    public long UserId { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal Quantity { get; set; }
}
