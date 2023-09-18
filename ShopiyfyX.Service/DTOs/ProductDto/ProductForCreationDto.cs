﻿namespace ShopiyfyX.Service.DTOs.ProductDto;

public class ProductForCreationDto
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public long CategoryId { get; set; }
    public long Quantity { get; set; }
}
