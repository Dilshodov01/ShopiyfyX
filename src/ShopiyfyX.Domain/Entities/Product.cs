using ShopiyfyX.Domain.Commons;

namespace ShopiyfyX.Domain.Entities;

public class Product : Auditable
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public long CategoryId { get; set; }
    public long Quantity { get; set; }
}
