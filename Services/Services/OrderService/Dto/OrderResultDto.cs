using Core.Entities.OrderEntities;

namespace Services.Services.OrderService.Dto
{
    public class OrderResultDto
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTimeOffset OerdreDate { get; set; }
        public AddressDto ShippingAddress { set; get; }
        public string DeliveryMethod { set; get; }
        public orderStats orderStats { set; get; } = orderStats.Pending;
        public IReadOnlyList<OrderItemDto> OrderItems { get; set; }
        public decimal SubTotal { get; set; }
        public decimal ShippingPrice { get; set; }
        public decimal Total { get; set; }
    }
}
