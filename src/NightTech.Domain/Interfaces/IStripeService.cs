namespace NightTech.Domain.Interfaces;

public interface IStripeService
{
    Task<string> CreatePaymentIntentAsync(decimal amount, Guid orderId);
}
