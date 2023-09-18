namespace ShopiyfyX.Service.DTOs.ProductDto;

public class ProductForCreationDto
{
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public long CategoryId { get; set; }
    public decimal Quantity { get; set; }
}
