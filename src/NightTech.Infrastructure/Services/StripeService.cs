using Microsoft.Extensions.Configuration;
using NightTech.Domain.Interfaces;
using Stripe;

namespace NightTech.Infrastructure.Services;

public class StripeService : IStripeService
{
    public StripeService(IConfiguration config)
    {
        StripeConfiguration.ApiKey = config["Stripe:SecretKey"];
    }

    public async Task<string> CreatePaymentIntentAsync(decimal amount, Guid orderId)
    {
        var options = new PaymentIntentCreateOptions
        {
            Amount = (long)(amount * 100), // cents
            Currency = "usd",
            Metadata = new Dictionary<string, string>
            {
                { "OrderId", orderId.ToString() },
            },
            AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
            {
                Enabled = true
            }
        };

        var service = new PaymentIntentService();
        var intent = await service.CreateAsync(options);
        return intent.ClientSecret;
    }
}
