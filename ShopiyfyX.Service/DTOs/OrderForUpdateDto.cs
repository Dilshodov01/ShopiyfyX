namespace ShopiyfyX.Service.DTOs;

public class OrderForUpdateDto
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public decimal TotalAmount { get; set; }
}
