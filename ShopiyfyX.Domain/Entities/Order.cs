using ShopiyfyX.Domain.Commons;

namespace ShopiyfyX.Domain.Entities;

public class Order : Auditable
{
    public long UserId { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal Quantity { get; set; }
}
