using NightTech.Domain.Constants;

namespace NightTech.Domain.Entities;

public class Order
{
    public Guid Id { get; set; }

    // Who placed the order
    public Guid UserId { get; set; } = default!;
    public virtual User User { get; set; } = default!;

    // Stripe’s Payment Intent / Checkout Session ID (to track payment)
    public string? PaymentIntentId { get; set; }

    // List of products in the order
    public ICollection<OrderItem> Items { get; set; } = default!;

    // Pricing
    public decimal TotalPrice { get; set; }
    public string Currency { get; set; } = "usd";

    // Status flow: Pending → Paid → Shipped → Completed / Cancelled
    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    // Metadata
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
