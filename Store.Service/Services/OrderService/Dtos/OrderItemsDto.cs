namespace Store.Service.Services.OrderService.Dtos
{
    public class OrderItemsDto
    {
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int ProductItemId { get; set; }
        public string ProductItemName { get; set; }
        public string PictureUrl { get; set; }
        public Guid OrderId { get; set; }
    }
}
