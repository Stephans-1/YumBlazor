using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;
using Stripe.Climate;
using EShop.Data;
using EShop.Repository.IRepository;
using EShop.Utility;

namespace EShop.Services
{
    public class PaymentService
    {
        private readonly NavigationManager _navManager;
        private readonly IOrderRepository _orderRepo;

        public PaymentService(NavigationManager _navigationManager, IOrderRepository _orderRepository)
        {
            _navManager = _navigationManager;
            _orderRepo = _orderRepository;
        }

        public Session CreateStripeCheckoutSession(OrderHeader orderHeader)
        {
            var lineItems = orderHeader.OrderDetails
                .Select(order => new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "usd",
                        UnitAmountDecimal = (decimal?)order.Price * 100,
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = order.ProductName
                        }
                    },
                    Quantity = order.Count
                }).ToList();

            var options = new Stripe.Checkout.SessionCreateOptions
            {
                SuccessUrl = $"{_navManager.BaseUri}order/confirmation/{{CHECKOUT_SESSION_ID}}",
                CancelUrl = $"{_navManager.BaseUri}cart",
                LineItems = lineItems,
                Mode = "payment"
            };

            var service = new SessionService();
            var session = service.Create(options);

            return session;
        }

        public async Task<OrderHeader> CheckPaymentStatusAndUpdateOrder(string sessionId)
        {
            OrderHeader orderHeader = await _orderRepo.GetOrderBySessionIdAsync(sessionId);
            var service = new SessionService();
            var session = service.Get(sessionId);
            if(session.PaymentStatus.ToLower() == "paid")
            {
                await _orderRepo.UpdateStatusAsync(orderHeader.Id, SD.StatusApproved, session.PaymentStatus);
            }
            return orderHeader;
        }



    }
}
