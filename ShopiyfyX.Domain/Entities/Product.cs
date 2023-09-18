using ShopiyfyX.Domain.Commons;

namespace ShopiyfyX.Domain.Entities;

public class Product : Auditable
{
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public long CategoryId { get; set; }
    public decimal Quantity { get; set; }
}
