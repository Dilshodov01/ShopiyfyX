﻿namespace ShopiyfyX.Service.DTOs;

public class ProductForUpdateDto
{
    public long Id { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
}
