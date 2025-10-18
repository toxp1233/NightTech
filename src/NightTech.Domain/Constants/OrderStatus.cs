namespace NightTech.Domain.Constants;

public enum OrderStatus
{
    Pending,   // Created but not paid yet
    Paid,      // Stripe payment confirmed
    Shipped,   // If physical product
    Completed, // Delivered / finished
    Cancelled
}
