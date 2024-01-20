namespace Core.Entities.OrderEntities
{
    public enum orderStats
    {
        Pending,
        PaymentReceived,
        PaymentFailed
    }
    public class Order : BaseEntity
    {
        public Order()
        {

        }
        public Order(string buyerEmail, ShippingAddress shippingAddress, DeliveryMethod deliveryMethod, IReadOnlyList<OrderItem> orderItems, decimal subTotal, string paymentIntentId)
        {
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            DeliveryMethod = deliveryMethod;
            OrderItems = orderItems;
            SubTotal = subTotal;
            PaymentIntentId = paymentIntentId;
        }

        public string BuyerEmail { get; set; }
        public DateTimeOffset OerdreDate { get; set; } = DateTimeOffset.Now;
        public ShippingAddress ShippingAddress { set; get; }
        public DeliveryMethod  DeliveryMethod{ set; get; }
        public orderStats orderStats { set; get; }  = orderStats.Pending;
        public IReadOnlyList<OrderItem> OrderItems { get; set; }
        public decimal SubTotal { get; set; }
        public string? PaymentIntentId { get; set; }
        public decimal GetTotal()
            => SubTotal + DeliveryMethod.Price;
    }
}
