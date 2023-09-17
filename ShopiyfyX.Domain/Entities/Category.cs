using ShopiyfyX.Domain.Commons;

namespace ShopiyfyX.Domain.Entities;

public class Category : Auditable
{
    public string CategoryName { get; set; }
    public decimal Quantity { get; set; }
}
