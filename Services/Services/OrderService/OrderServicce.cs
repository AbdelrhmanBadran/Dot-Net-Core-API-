using AutoMapper;
using Core.Entities;
using Core.Entities.OrderEntities;
using Infrastructure.Interfaces;
using Infrastructure.Specifications;
using Services.Services.BasketService;
using Services.Services.OrderService.Dto;
using Services.Services.PaymentService;
using Product = Core.Entities.Product;

namespace Services.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IBasketService _basketService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;
        private readonly IPaymentService _paymentService;

        public OrderService(IBasketService basketService , IUnitOfWork unitOfWork , IMapper mapper , IPaymentService paymentService)
        {
            _basketService = basketService;
            _unitOfWork = unitOfWork;
            this.mapper = mapper;
            _paymentService = paymentService;
        }
        public async Task<OrderResultDto> CreateOrderAsync(OrderDto orderDto)
        {
            //Get Basket
            var Basket = await _basketService.GetBasketAsync(orderDto.BasketId);

            if (Basket == null)
                return null;


            //Fill orderItem from BasketItem
            var OrderItems = new List<OrderItemDto>();

            foreach (var item in Basket.BasketItems)
            {
                var productItem = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                var itemOrder = new ProductItemOrdered(productItem.Id , productItem.Name , productItem.PictureUrl);
                var orderItem = new OrderItem(productItem.Price, item.Quantity , itemOrder);

                var mappedItem = mapper.Map<OrderItemDto>(orderItem);

                OrderItems.Add(mappedItem);

            }

            // Get Delivery method

            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(orderDto.DeliveryMethodId);

            // Calc subtotal

            var subTotal = OrderItems.Sum(item => item.Price * item.Quantity);

            // check if order exist

            var specs = new OrderWithPaymentSpecifications(Basket.PaymentIntentId);

            var existingOrder = await _unitOfWork.Repository<Order>().GetEntityWithSpecificationsAsync(specs);

            if (existingOrder != null)
            {
                _unitOfWork.Repository<Order>().Delete(existingOrder);
                await _paymentService.CreateOrUpdatePaymentIntent(Basket.PaymentIntentId);
            }

            // create order
            var mappedshippedAddress = mapper.Map<ShippingAddress>(orderDto.ShippingAddress);
            var mappedItems = mapper.Map<List<OrderItem>>(OrderItems);

            var order = new Order(orderDto.BuyerEmail , mappedshippedAddress, deliveryMethod , mappedItems, subTotal , Basket.PaymentIntentId);

            await _unitOfWork.Repository<Order>().Add(order);

            await _unitOfWork.Complete();

            var mappedOrder = mapper.Map<OrderResultDto>(order);

            return mappedOrder; 
        }

        public async Task<IReadOnlyList<OrderResultDto>> GetAllOrdersForUsersAsync(string buyerEmail)
        {
            var specs = new OrderWithItemsSpecifications(buyerEmail);

            var orders = _unitOfWork.Repository<Order>().GetAllyWithSpecificationsAsync(specs);

            var mappedOrders = mapper.Map<List<OrderResultDto>>(orders);

            return mappedOrders;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync()
            => await _unitOfWork.Repository<DeliveryMethod>().GetByAllAsync();
        

        public async Task<OrderResultDto> GetOrderByIdAsync(int id, string buyerEmail)
        {
            var specs = new OrderWithItemsSpecifications(id , buyerEmail);

            var orders = _unitOfWork.Repository<Order>().GetEntityWithSpecificationsAsync(specs);

            var mappedOrders = mapper.Map<OrderResultDto>(orders);

            return mappedOrders;
        }
    }
}
