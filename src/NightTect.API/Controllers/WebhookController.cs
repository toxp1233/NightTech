using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NightTech.Application.Orders.Commands.UpdateOrder;
using NightTech.Domain.Constants;
using Stripe;

namespace NightTech.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class WebhookController(IMediator mediator, IConfiguration config) : ControllerBase
{
    private readonly string _webhookSecret = config["Stripe:WebhookSecret"]!;

    [HttpPost("listener")]
    public async Task<IActionResult> WebHookListener()
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
        var signatureHeader = Request.Headers["Stripe-Signature"];

        if (string.IsNullOrEmpty(signatureHeader))
            return BadRequest("Missing Stripe signature header");

        try
        {
            var stripeEvent = EventUtility.ConstructEvent(json, signatureHeader, _webhookSecret);

            switch (stripeEvent.Type)
            {
                case EventTypes.PaymentIntentSucceeded:
                    var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                    if (paymentIntent?.Metadata != null &&
                        paymentIntent.Metadata.TryGetValue("OrderId", out var orderIdStr) &&
                        Guid.TryParse(orderIdStr, out var orderId))
                    {
                        await mediator.Send(new UpdateOrderCommand(orderId, OrderStatus.Paid));
                    }
                    break;

                case EventTypes.PaymentMethodAttached:
                    // optional: handle if you save payment methods
                    break;

                default:
                    Console.WriteLine($"Unhandled Stripe event type: {stripeEvent.Type}");
                    break;
            }

            return Ok();
        }
        catch (StripeException e)
        {
            Console.WriteLine($"Stripe error: {e.Message}");
            return BadRequest();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Webhook processing error: {e.Message}");
            return StatusCode(500);
        }
    }
}
