using ShopiyfyX.Domain.Commons;

namespace ShopiyfyX.Domain.Entities;

public class OrderItem : Auditable
{
    public long OrderId { get; set; }
    public long ProductId { get; set; }
}
