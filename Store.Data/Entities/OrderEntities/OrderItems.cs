namespace Store.Data.Entities.OrderEntities
{
    public class OrderItems : BaseEntity<Guid>
    {
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public ProductOrderedItem orderedItem { get; set; }
        public Guid OrderId { get; set; }
    }
}