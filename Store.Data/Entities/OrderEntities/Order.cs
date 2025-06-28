namespace Store.Data.Entities.OrderEntities
{
    public enum OrderPayementStatus
    {
        Pending,
        Received,
        Failed
    }
    public class Order : BaseEntity<Guid>
    {
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public ShippingAddress ShippingAddress { get; set; }
        public DeliveryMethod? DeliveryMethod { get; set; }
        public int? DeliveryMethodId { get; set; }
        public OrderPayementStatus OrderPayementStatus { get; set; } = OrderPayementStatus.Pending;
        public IReadOnlyList<OrderItems> OrderItems { get; set; }
        public decimal SubTotal { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? BasketId { get; set; }
        public decimal GetTotal()
            => SubTotal + DeliveryMethod.Price;
    }
}
