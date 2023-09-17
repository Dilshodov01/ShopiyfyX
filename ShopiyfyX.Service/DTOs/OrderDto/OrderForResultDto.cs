namespace ShopiyfyX.Service.DTOs.OrderDto;

public class OrderForResultDto
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal Quantity { get; set; }
}
