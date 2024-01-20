using AutoMapper;
using Core.Entities;
using Core.Entities.OrderEntities;
using Infrastructure.Interfaces;
using Infrastructure.Specifications;
using Microsoft.Extensions.Configuration;
using Services.Services.BasketService;
using Services.Services.BasketService.Dto;
using Services.Services.OrderService.Dto;
using Stripe;
using Product = Core.Entities.Product;

namespace Services.Services.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IMapper mapper;
        private readonly IBasketService _basketService;

        public PaymentService(IUnitOfWork unitOfWork , IBasketService basketService , IConfiguration configuration , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            this.mapper = mapper;
            _basketService = basketService;
        }


        public async Task<CustomerBasketDto> CreateOrUpdatePaymentIntent(string basketId)
        {
            StripeConfiguration.ApiKey = _configuration["Stripe : SecretKey"];

            var basket = await _basketService.GetBasketAsync(basketId);

            if (basket == null)
                return null;

            var ShippingPrice = 0m;

            if (basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(basket.DeliveryMethodId.Value);
                ShippingPrice = deliveryMethod.Price;
            }

            foreach (var item in basket.BasketItems)
            {
                var productItems = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                if (item.Price != productItems.Price)
                    item.Price = productItems.Price;
            }

            var service = new PaymentIntentService();

            PaymentIntent intent;

            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)basket.BasketItems.Sum(item => item.Quantity * (item.Price * 100)) +(long)(ShippingPrice * 100),
                    Currency = "USD",
                    PaymentMethodTypes = new List<string> { "card" }
                };

                intent = await service.CreateAsync(options);

                basket .PaymentIntentId = intent.Id;
                basket.ClientSecret = intent.ClientSecret;


            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long)basket.BasketItems.Sum(item => item.Quantity * (item.Price * 100)) + (long)(ShippingPrice * 100),
                };
                await service.UpdateAsync(basket.PaymentIntentId,options);

            }

            await _basketService.UpdateBasketAsync(basket);

            return basket;


        }

        public async Task<OrderResultDto> UpdateOrderPaymentFailed(string paymentIntentId)
        {
            var specs = new OrderWithPaymentSpecifications(paymentIntentId);

            var order = await _unitOfWork.Repository<Order>().GetEntityWithSpecificationsAsync(specs);

            if (order == null)
                return null;

            order.orderStats = orderStats.PaymentFailed;

            _unitOfWork.Repository<Order>().Update(order);

            await _unitOfWork.Complete();

            var mappedOrder = mapper.Map<OrderResultDto>(order);

            return mappedOrder;

        }

        public async Task<OrderResultDto> UpdateOrderPaymentSucceeded(string paymentIntentId , string basketId)
        {
            var specs = new OrderWithPaymentSpecifications(paymentIntentId);

            var order = await _unitOfWork.Repository<Order>().GetEntityWithSpecificationsAsync(specs);

            if (order == null)
                return null;

            order.orderStats = orderStats.PaymentReceived;

            _unitOfWork.Repository<Order>().Update(order);

            await _unitOfWork.Complete();

            await _basketService.DeleteBasketAsync(basketId);

            var mappedOrder = mapper.Map<OrderResultDto>(order);

            return mappedOrder;

        }

        public Task<OrderResultDto> UpdateOrderPaymentSucceeded(string paymentIntentId)
        {
            throw new NotImplementedException();
        }
    }
}
