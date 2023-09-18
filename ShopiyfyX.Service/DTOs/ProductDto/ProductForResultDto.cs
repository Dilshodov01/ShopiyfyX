namespace ShopiyfyX.Service.DTOs.ProductDto;

public class ProductForResultDto
{
    public long Id { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public long CategoryId { get; set; }
    public decimal Quantity { get; set; }
}
